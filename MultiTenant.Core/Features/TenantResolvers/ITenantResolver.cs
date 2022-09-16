namespace MultiTenant.Core.Features.TenantResolvers;

/// <summary>
/// Tenant resolver. 
/// </summary>
public interface ITenantResolver
{
    /// <summary>
    /// Implement this method to customize how you retrieve your tenant's code.
    /// </summary>
    /// <param name="context">Http context used to retrieve tenant's code</param>
    /// <returns>Tenant's data</returns>
    Task<Tenant?> ResolveTenantCode(HttpContext context);
}
