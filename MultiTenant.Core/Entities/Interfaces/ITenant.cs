namespace MultiTenant.Core.Entities.Interfaces;

/// <summary>
/// Used to identify entity's tenant
/// </summary>
public interface ITenant
{
    /// <summary>
    /// Key used to store tenant(s) identifier(s)
    /// </summary>
    string? TenantKey { get; set; }
}
