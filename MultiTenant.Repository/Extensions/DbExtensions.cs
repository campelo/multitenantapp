namespace MultiTenant.Repository.Extensions;

/// <summary>
/// Data base extensions
/// </summary>
public static class DbExtensions
{
    /// <summary>
    /// Apply all rules for multi-tenant entities
    /// </summary>
    /// <param name="dbContext">Data base context</param>
    /// <param name="tenantKey">Current tenant's key</param>
    public static void ApplyMultitenantRules(this DbContext dbContext, string? tenantKey)
    {
        dbContext.ApplyRulesForAddedItems(tenantKey);
        dbContext.ApplyRulesForDeletedItems();
    }

    /// <summary>
    /// Add tenant key before adding new entity
    /// </summary>
    /// <param name="dbContext">Data base context</param>
    /// <param name="tenantKey">Current tenant's key</param>
    public static void ApplyRulesForAddedItems(this DbContext dbContext, string? tenantKey)
    {
        foreach (var entry in dbContext.ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
        {
            Type entryType = entry.Entity.GetType();
            if (typeof(IHasHierarchicalTenant).IsAssignableFrom(entryType))
            {
                var entity = entry.Entity as IHasHierarchicalTenant;
                if (entity is not null && string.IsNullOrWhiteSpace(entity.TenantKey))
                    entity.TenantKey = tenantKey;
            }
            else if (typeof(ISharedInTenant).IsAssignableFrom(entryType))
            {
                var entity = entry.Entity as ISharedInTenant;
                if (entity is not null)
                    entity.TenantKey = tenantKey.RetrieveMainTenantKey() ?? entity.TenantKey.RetrieveMainTenantKey();
            }
            else if (typeof(IMustHaveTenant).IsAssignableFrom(entryType))
            {
                var entity = entry.Entity as IMustHaveTenant;
                if (!(entity == null || string.IsNullOrWhiteSpace(tenantKey)))
                    entity.TenantKey = tenantKey;
            }
            if (typeof(IHasOptionsHandler).IsAssignableFrom(entryType))
            {
                var entity = entry.Entity as IHasOptionsHandler;
                if (entity is not null)
                    entity.Options = EntityOptions.Active;
            }
        }
    }

    /// <summary>
    /// Apply multi-tenant rules for deleted items
    /// </summary>
    /// <param name="dbContext">Data base context</param>
    public static void ApplyRulesForDeletedItems(this DbContext dbContext)
    {
        foreach (var entry in dbContext.ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted))
        {
            Type entryType = entry.Entity.GetType();
            if (typeof(IHasOptionsHandler).IsAssignableFrom(entryType))
            {
                entry.State = EntityState.Modified;
                var entity = entry.Entity as IHasOptionsHandler;
                if (entity is not null)
                    entity.Options = EntityOptions.Deleted;
            }
        }
    }

    /// <summary>
    /// Filter query results by tenant key
    /// </summary>
    /// <param name="entity">Searching entity</param>
    /// <param name="tenantData">Object containing tenant key</param>
    public static void ApplyTenantRules(this IMutableEntityType entity)
    {
        entity.GetProperty(nameof(ITenant.TenantKey)).SetIsUnicode(false); //Make unicode
        entity.GetProperty(nameof(ITenant.TenantKey)).SetMaxLength(RepositoryContants.HIERACHYCAL_TENANT_ID_LENGTH); //bigger for hierarchical multi-tenant
        entity.AddIndex(entity.FindProperty(nameof(ITenant.TenantKey)));
    }

    /// <summary>
    /// Filter query results by hierarchy from a tenant key
    /// </summary>
    /// <param name="entity">Searching entity</param>
    /// <param name="tenantData">Object containing tenant key</param>
    public static void ApplyHierarchicalTenantRules(this IMutableEntityType entity)
    {
        entity.GetProperty(nameof(ITenant.TenantKey)).SetIsUnicode(false); //Make unicode
        entity.GetProperty(nameof(ITenant.TenantKey)).SetMaxLength(RepositoryContants.HIERACHYCAL_TENANT_ID_LENGTH); //bigger for hierarchical multi-tenant
        entity.AddIndex(entity.FindProperty(nameof(ITenant.TenantKey)));
    }

    /// <summary>
    /// Filter query results for shared items by tenant key
    /// </summary>
    /// <param name="entity">Searching entity</param>
    /// <param name="tenantData">Object containing tenant key</param>
    public static void ApplySharedInsideTenantRules(this IMutableEntityType entity)
    {
        entity.GetProperty(nameof(ITenant.TenantKey)).SetIsUnicode(false); //Make unicode
        entity.GetProperty(nameof(ITenant.TenantKey)).SetMaxLength(RepositoryContants.SINGLE_TENANT_ID_LENGTH); //smaller for single tenant
        entity.AddIndex(entity.FindProperty(nameof(ITenant.TenantKey)));
    }

    public static void ApplyOptionsHandlerRules(this IMutableEntityType entity)
    {
        entity.AddIndex(entity.FindProperty(nameof(IHasOptionsHandler.Options)));
    }

    private static string RetrieveMainTenantKey(this string tenantKey)
    {
        if (string.IsNullOrWhiteSpace(tenantKey) || !tenantKey.Contains(Tenant.TENANT_KEY_SPLITTER))
            return tenantKey;
        return tenantKey.Split(Tenant.TENANT_KEY_SPLITTER)[0] + Tenant.TENANT_KEY_SPLITTER;
    }

    public static void ApplyQueryFilter(this IMutableEntityType entity, ITenant tenantData, List<Type> filterTypes)
    {
        var methodToCall = typeof(DbExtensions)
            .GetMethod(nameof(ApplyAllFilters),
                BindingFlags.NonPublic | BindingFlags.Static)
            .MakeGenericMethod(entity.ClrType);
        var filter = methodToCall.Invoke(null, new object[] { tenantData, filterTypes });
        if (filter == null)
            return;
        entity.SetQueryFilter((LambdaExpression)filter);
    }

    private static LambdaExpression ApplyAllFilters<TEntity>(ITenant tenantData, List<Type> filterTypes)
        where TEntity : class
    {
        List<Expression<Func<TEntity, bool>>> expressions = new();
        if (filterTypes.Contains(typeof(IMustHaveTenant)))
            expressions.Add(SetupMustHaveTenantFilter<TEntity>(tenantData));
        if (filterTypes.Contains(typeof(IHasHierarchicalTenant)))
            expressions.Add(SetupHierarchicalTenantQueryFilter<TEntity>(tenantData));
        if (filterTypes.Contains(typeof(ISharedInTenant)))
            expressions.Add(SetupSharedInTenantQueryFilter<TEntity>(tenantData));
        if (filterTypes.Contains(typeof(IHasOptionsHandler)))
            expressions.Add(SetupOptionsHandlerQueryFilter<TEntity>());

        if (!expressions.Any())
            return null;

        Expression<Func<TEntity, bool>> result = expressions.First();
        for (int i = 1; i < expressions.Count(); i++)
        {
            result = result.And(expressions[i]);
        }
        return result;
    }

    private static Expression<Func<TEntity, bool>> SetupMustHaveTenantFilter<TEntity>(ITenant tenantData)
        where TEntity : class
    {
        Expression<Func<TEntity, bool>> filter = x => (x as IMustHaveTenant).TenantKey.Equals(tenantData.TenantKey);
        return filter;
    }

    private static Expression<Func<TEntity, bool>> SetupHierarchicalTenantQueryFilter<TEntity>(ITenant tenantData)
        where TEntity : class
    {
        Expression<Func<TEntity, bool>> filter = x => (x as IHasHierarchicalTenant).TenantKey.StartsWith(tenantData.TenantKey);
        return filter;
    }

    private static Expression<Func<TEntity, bool>> SetupSharedInTenantQueryFilter<TEntity>(ITenant tenantData)
        where TEntity : class
    {
        Expression<Func<TEntity, bool>> filter = x => (x as ISharedInTenant).TenantKey.StartsWith(tenantData.TenantKey.RetrieveMainTenantKey());
        return filter;
    }

    private static Expression<Func<TEntity, bool>> SetupOptionsHandlerQueryFilter<TEntity>()
        where TEntity : class
    {
        Expression<Func<TEntity, bool>> filter = x => (x as IHasOptionsHandler).Options != EntityOptions.Deleted;
        return filter;
    }

}
