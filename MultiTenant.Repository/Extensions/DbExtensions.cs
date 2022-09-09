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
                var hasTenantId = entry.Entity as IMustHaveTenant;
                if (hasTenantId is not null && string.IsNullOrWhiteSpace(hasTenantId.TenantKey))
                    hasTenantId.TenantKey = tenantId;
            }
        }

        public static void AddTenantQueryFilter(this IMutableEntityType entity, IMustHaveTenant tenantData)
        {
            var methodToCall = typeof(DbExtensions)
                .GetMethod(nameof(SetupTenantQueryFilter),
                    BindingFlags.NonPublic | BindingFlags.Static)
                .MakeGenericMethod(entity.ClrType);
            var filter = methodToCall.Invoke(null, new object[] { tenantData });
            entity.SetQueryFilter((LambdaExpression)filter);
            entity.GetProperty(nameof(IMustHaveTenant.TenantKey)).SetIsUnicode(false); //Make unicode
            entity.GetProperty(nameof(IMustHaveTenant.TenantKey)).SetMaxLength(RepositoryContants.TENANT_ID_LENGTH); //bigger for hierarchical multi-tenant
            entity.AddIndex(entity.FindProperty(nameof(tenantData.TenantKey)));
        }

        private static LambdaExpression SetupTenantQueryFilter<TEntity>(IMustHaveTenant tenantData)
            where TEntity : class, IMustHaveTenant
        {
            Expression<Func<TEntity, bool>> filter = x => x.TenantKey.StartsWith(tenantData.TenantKey);
            return filter;
        }
    }
}
