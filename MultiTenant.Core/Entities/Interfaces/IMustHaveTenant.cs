namespace MultiTenant.Core.Entities.Interfaces
{
    public interface IMustHaveTenant
    {
        string TenantKey { get; set; }
    }
}
