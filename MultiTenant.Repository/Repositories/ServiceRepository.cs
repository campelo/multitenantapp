namespace MultiTenant.Repository.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly MultiTenantDbContext _dbContext;

    public ServiceRepository(MultiTenantDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Service> Create(Service service)
    {
        await _dbContext.Services.AddAsync(service);
        _dbContext.SaveChanges();
        return service;
    }

    public async Task<IEnumerable<Service>> GetAll()
    {
        return await _dbContext.Services.ToListAsync();
    }
}
