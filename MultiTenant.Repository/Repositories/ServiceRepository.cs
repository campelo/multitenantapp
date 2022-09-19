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
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task Delete(int id)
    {
        Service? entity = await _dbContext.Services.FirstOrDefaultAsync(x => x.Id == id);
        _dbContext.Services.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Service>> GetAll()
    {
        return await _dbContext.Services.ToListAsync();
    }
}
