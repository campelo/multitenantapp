namespace MultiTenant.Core.Repositories.Interfaces;

public interface ITenantRepository
{
    Task<IEnumerable<Tenant>> GetAll();
    Task<Tenant?> GetByCode(string tenantCode);
}
