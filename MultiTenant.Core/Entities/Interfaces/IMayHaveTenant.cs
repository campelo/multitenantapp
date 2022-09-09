namespace MultiTenant.Core.Entities.Interfaces
{
    public interface IMayHaveTenant
    {
        string? TenantId { get; set; }
    }
}
