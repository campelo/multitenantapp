namespace MultiTenant.Repository.Repositories;

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

    public async Task<Tenant?> GetByCode(string tenantCode)
    {
        tenantCode = tenantCode.Trim().ToLower();
        var result = await _dbContext
            .Tenants
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Code == tenantCode);
        return result;
    }
}
