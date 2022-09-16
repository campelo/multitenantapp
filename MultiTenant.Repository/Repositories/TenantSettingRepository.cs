namespace MultiTenant.Repository.Repositories;

public class TenantSettingRepository : ITenantSettingRepository
{
    private readonly MultiTenantDbContext _dbContext;

    public TenantSettingRepository(MultiTenantDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TenantSetting> Create(TenantSetting entity)
    {
        await _dbContext.TenantSettings.AddAsync(entity);
        _dbContext.SaveChanges();
        return entity;
    }

    public async Task<IEnumerable<TenantSetting>> GetAll()
    {
        return await _dbContext.TenantSettings.AsNoTracking().ToListAsync();
    }
}
