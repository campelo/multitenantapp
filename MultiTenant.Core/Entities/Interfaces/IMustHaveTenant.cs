namespace MultiTenant.Core.Entities.Interfaces;

/// <summary>
/// An entity implementing this interface could only be seen by its own tenant...
/// </summary>
public interface IMustHaveTenant : ITenant
{
    /// <inheritdoc />
    new string TenantKey { get; set; }
}
