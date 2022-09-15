namespace MultiTenant.App.Features.TenantResolvers;

/// <summary>
/// This tenant resolver will resolve tenant by path. 
/// </summary>
/// <remarks>
/// <see cref="MultiTenantApiControllerAttribute"/> in API controllers will make this work easy and no more implementation will be required.
/// </remarks>
public class PathTenantResolver : ITenantResolver
{
    private readonly ITenantService _tenantService;

    public PathTenantResolver(ITenantService tenantService)
    {
        _tenantService = tenantService;
    }

    /// <inheritdoc/>
    public async Task<string?> ResolveTenantCode(HttpContext httpContext)
    {
        if (httpContext is null ||
            !httpContext.Request.RouteValues.Any(x => x.Key.Equals(AppConstantsSingleton.Instance.TenantIdentifier, StringComparison.OrdinalIgnoreCase)))
            return null;
        string? tenantCode = httpContext.Request.RouteValues.First(x => x.Key.Equals(AppConstantsSingleton.Instance.TenantIdentifier, StringComparison.OrdinalIgnoreCase)).Value?.ToString();

        if (string.IsNullOrWhiteSpace(tenantCode))
            return null;

        Tenant tenant = await _tenantService.GetByCode(tenantCode);

        return tenant?.GetTenantKey;
    }
}
