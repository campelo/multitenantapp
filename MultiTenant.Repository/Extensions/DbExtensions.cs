using Microsoft.EntityFrameworkCore.Metadata;
using MultiTenant.Core.Entities.Interfaces;
using System.Linq.Expressions;
using System.Reflection;

namespace MultiTenant.Repository.Extensions
{
    public static class DbExtensions
    {
        public static void AddTenantQueryFilter(this IMutableEntityType entity, IMustHaveTenant tenantData)
        {
            var methodToCall = typeof(DbExtensions)
                .GetMethod(nameof(SetupTenantQueryFilter),
                    BindingFlags.NonPublic | BindingFlags.Static)
                .MakeGenericMethod(entity.ClrType);
            var filter = methodToCall.Invoke(null, new object[] { tenantData });
            entity.SetQueryFilter((LambdaExpression)filter);
            entity.AddIndex(entity.FindProperty(nameof(tenantData.TenantId)));
        }

        private static LambdaExpression SetupTenantQueryFilter<TEntity>(IMustHaveTenant tenantData)
            where TEntity : class, IMustHaveTenant
        {
            Expression<Func<TEntity, bool>> filter = x => x.TenantId == tenantData.TenantId;
            return filter;
        }
    }
}
