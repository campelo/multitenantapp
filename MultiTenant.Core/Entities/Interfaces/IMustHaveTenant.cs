namespace MultiTenant.Core.Entities.Interfaces;

/// <summary>
/// An entity implementing this interface could only be seen by its own tenant and its parents...
/// </summary>
public interface IMustHaveTenant : ITenant
{
    string TenantKey { get; set; }
}
