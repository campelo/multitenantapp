namespace MultiTenant.Core.Features.Contexts;

/// <summary>
/// Keep global information to be used with multi-tenant applications.
/// </summary>
public interface IGlobalContext
{
    /// <summary>
    /// Tenant's key used to handle multi-tenant data
    /// </summary>
    public string? TenantKey { get; }

    /// <summary>
    /// Set global context values
    /// </summary>
    /// <param name="tenantKey">Tenant's key</param>
    void SetContext(string? tenantKey);
}
