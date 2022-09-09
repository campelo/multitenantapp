using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MultiTenant.Core.Entities.Interfaces;
using System.Linq.Expressions;
using System.Reflection;

namespace MultiTenant.Repository.Extensions
{
    public static class DbExtensions
    {
        public static void AddTenantIfNeeded(this DbContext dbContext, string? tenantId)
        {
            foreach (var entry in dbContext.ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
            {
                var hasTenantId = entry.Entity as IMayHaveTenant;
                if (hasTenantId is not null && hasTenantId.TenantId is null)
                    hasTenantId.TenantId = tenantId;
            }
        }

        public static void AddTenantQueryFilter(this IMutableEntityType entity, IMayHaveTenant tenantData)
        {
            var methodToCall = typeof(DbExtensions)
                .GetMethod(nameof(SetupTenantQueryFilter),
                    BindingFlags.NonPublic | BindingFlags.Static)
                .MakeGenericMethod(entity.ClrType);
            var filter = methodToCall.Invoke(null, new object[] { tenantData });
            entity.SetQueryFilter((LambdaExpression)filter);
            entity.AddIndex(entity.FindProperty(nameof(tenantData.TenantId)));
        }

        private static LambdaExpression SetupTenantQueryFilter<TEntity>(IMayHaveTenant tenantData)
            where TEntity : class, IMustHaveTenant
        {
            Expression<Func<TEntity, bool>> filter = x => x.TenantId == tenantData.TenantId;
            return filter;
        }
    }
}
