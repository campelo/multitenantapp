namespace MultiTenant.Core.Entities;

/// <summary>
/// Tenant settings.
/// </summary>
public class TenantSetting : IMustHaveTenant
{
    /// <summary>
    /// Setting's value
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Setting's value
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Tenant's ID
    /// </summary>
    public int TenantId { get; set; }

    /// <summary>
    /// Tenant
    /// </summary>
    public Tenant Tenant { get; set; }

    /// <inheritdoc/>
    public string TenantKey { get; set; }
}
