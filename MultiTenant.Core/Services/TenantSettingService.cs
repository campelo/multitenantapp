namespace MultiTenant.Core.Services
{
    public class TenantSettingService : ITenantSettingService
    {
        private readonly ITenantSettingRepository _tenantSettingRepository;

        public TenantSettingService(ITenantSettingRepository tenantSettingRepository)
        {
            _tenantSettingRepository = tenantSettingRepository;
        }

        public Task<TenantSetting> Create(TenantSetting entity)
        {
            return _tenantSettingRepository.Create(entity);
        }

        public Task<IEnumerable<TenantSetting>> GetAll()
        {
            return _tenantSettingRepository.GetAll();
        }
    }
}
