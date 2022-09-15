namespace MultiTenant.Core.Entities.Interfaces;

public interface ITenant
{
    string? TenantKey { get; set; }
}
