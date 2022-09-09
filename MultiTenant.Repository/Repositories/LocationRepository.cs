using Microsoft.EntityFrameworkCore;
using MultiTenant.Core.Entities;
using MultiTenant.Core.Repositories.Interfaces;
using MultiTenant.Repository.DbContexts;

namespace MultiTenant.Repository.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly MultiTenantDbContext _dbContext;

        public LocationRepository(MultiTenantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Location> Create(Location location)
        {
            await _dbContext.Locations.AddAsync(location);
            _dbContext.SaveChanges();
            return location;
        }

        public async Task<IEnumerable<Location>> GetAll()
        {
            return await _dbContext.Locations.ToListAsync();
        }
    }
}
