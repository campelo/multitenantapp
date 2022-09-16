namespace MultiTenant.Repository.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly MultiTenantDbContext _dbContext;

    public ServiceRepository(MultiTenantDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Service> Create(Service entity)
    {
        await _dbContext.Services.AddAsync(entity);
        _dbContext.SaveChanges();
        return entity;
    }

    public async Task<IEnumerable<Service>> GetAll()
    {
        return await _dbContext.Services.ToListAsync();
    }
}
