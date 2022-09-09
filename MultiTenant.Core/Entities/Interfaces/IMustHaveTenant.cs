namespace MultiTenant.Core.Entities.Interfaces
{
    public interface IMustHaveTenant : IMayHaveTenant
    {
        string TenantId { get; set; }
    }
}
