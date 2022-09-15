namespace MultiTenant.Core.Services.Interfaces;

public interface ITenantService
{
    Task<Tenant> GetByCode(string tenantCode);
}
