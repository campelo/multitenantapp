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
    /// Set global context tenant
    /// </summary>
    /// <param name="currentTenant">Current tenant's data</param>
    void SetCurrentTenant(Tenant? currentTenant);
}
