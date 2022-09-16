namespace MultiTenant.Core.Services;

public class TenantService : ITenantService
{
    private readonly ITenantRepository _tenantRepository;

    public TenantService(ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public async Task<Tenant?> GetByCode(string tenantCode)
    {
        return await _tenantRepository.GetByCode(tenantCode);
    }
}
