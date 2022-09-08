using MultiTenant.Core.Entities;
using MultiTenant.Core.Repositories.Interfaces;
using MultiTenant.Core.Services.Interfaces;

namespace MultiTenant.Core.Services
{
    public class TenantService : ITenantService
    {
        private readonly ITenantRepository _tenantRepository;

        public TenantService(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<Tenant> GetByName(string tenantName)
        {
            return await _tenantRepository.GetByName(tenantName);
        }
    }
}
