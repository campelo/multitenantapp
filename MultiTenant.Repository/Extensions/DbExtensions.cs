namespace MultiTenant.Repository.Extensions;

public static class DbExtensions
{
    public static void AddTenantIfNeeded(this DbContext dbContext, string? tenantId)
    {
        foreach (var entry in dbContext.ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
        {
            Type entryType = entry.Entity.GetType();
            if (typeof(IMustHaveTenant).IsAssignableFrom(entryType))
            {
                var hasTenantId = entry.Entity as IMustHaveTenant;
                if (hasTenantId is not null && string.IsNullOrWhiteSpace(hasTenantId.TenantKey))
                    hasTenantId.TenantKey = tenantId;
            }
            else if (typeof(ISharedInTenant).IsAssignableFrom(entryType))
            {
                var hasTenantId = entry.Entity as ISharedInTenant;
                if (hasTenantId is not null)
                    hasTenantId.TenantKey = tenantId.RetrieveMainTenantKey() ?? hasTenantId.TenantKey.RetrieveMainTenantKey();
            }
        }
    }

    public static void AddTenantQueryFilter(this IMutableEntityType entity, ITenant tenantData)
    {
        var methodToCall = typeof(DbExtensions)
            .GetMethod(nameof(SetupTenantQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static)
            .MakeGenericMethod(entity.ClrType);
        var filter = methodToCall.Invoke(null, new object[] { tenantData });
        entity.SetQueryFilter((LambdaExpression)filter);
        entity.GetProperty(nameof(ITenant.TenantKey)).SetIsUnicode(false); //Make unicode
        entity.GetProperty(nameof(ITenant.TenantKey)).SetMaxLength(RepositoryContants.HIERACHYCAL_TENANT_ID_LENGTH); //bigger for hierarchical multi-tenant
        entity.AddIndex(entity.FindProperty(nameof(tenantData.TenantKey)));
    }

    private static LambdaExpression SetupTenantQueryFilter<TEntity>(ITenant tenantData)
        where TEntity : class, IMustHaveTenant
    {
        Expression<Func<TEntity, bool>> filter = x => x.TenantKey.StartsWith(tenantData.TenantKey);
        return filter;
    }

    public static void AddSharedInsideTenantQueryFilter(this IMutableEntityType entity, ITenant tenantData)
    {
        var methodToCall = typeof(DbExtensions)
            .GetMethod(nameof(SetupSharedInsideTenantQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static)
            .MakeGenericMethod(entity.ClrType);
        var filter = methodToCall.Invoke(null, new object[] { tenantData });
        entity.SetQueryFilter((LambdaExpression)filter);
        entity.GetProperty(nameof(ITenant.TenantKey)).SetIsUnicode(false); //Make unicode
        entity.GetProperty(nameof(ISharedInTenant.TenantKey)).SetMaxLength(RepositoryContants.SINGLE_TENANT_ID_LENGTH); //smaller for single tenant
        entity.AddIndex(entity.FindProperty(nameof(tenantData.TenantKey)));
    }

    private static LambdaExpression SetupSharedInsideTenantQueryFilter<TEntity>(ITenant tenantData)
        where TEntity : class, ISharedInTenant
    {
        Expression<Func<TEntity, bool>> filter = x => x.TenantKey.StartsWith(tenantData.TenantKey.RetrieveMainTenantKey());
        return filter;
    }

    private static string RetrieveMainTenantKey(this string tenantKey)
    {
        if (string.IsNullOrWhiteSpace(tenantKey) || !tenantKey.Contains(Tenant.TENANT_KEY_SPLITTER))
            return tenantKey;
        return tenantKey.Split(Tenant.TENANT_KEY_SPLITTER)[0] + Tenant.TENANT_KEY_SPLITTER;
    }

}
