namespace MultiTenant.Core.Entities.Interfaces
{
    public interface IMustHaveTenant
    {
        string? TenantId { get; set; }
    }
}
