using MultiTenant.Core.Entities;

namespace MultiTenant.Core.Repositories.Interfaces
{
    public interface ITenantRepository
    {
        Task<IEnumerable<Tenant>> GetAll();
        Task<Tenant?> GetByName(string tenantName);
    }
}
