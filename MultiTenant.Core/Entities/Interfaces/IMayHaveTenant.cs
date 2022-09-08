namespace MultiTenant.Core.Entities.Interfaces
{
    public interface IMayHaveTenant
    {
        int? TenantId { get; set; }
    }
}
