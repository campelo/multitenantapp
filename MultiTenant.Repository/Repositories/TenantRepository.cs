using Microsoft.EntityFrameworkCore;
using MultiTenant.Core.Entities;
using MultiTenant.Core.Repositories.Interfaces;
using MultiTenant.Repository.DbContexts;

namespace MultiTenant.Repository.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly MultiTenantDbContext _dbContext;

        public TenantRepository(MultiTenantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Tenant>> GetAll()
        {
            return await _dbContext.Tenants.ToListAsync();
        }

        public async Task<Tenant?> GetByName(string tenantName)
        {
            tenantName = tenantName.Trim().ToLower();
            var result = await _dbContext.Tenants.FirstOrDefaultAsync(x => x.Name == tenantName);
            return result;
        }
    }
}
