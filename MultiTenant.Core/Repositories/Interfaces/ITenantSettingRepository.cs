namespace MultiTenant.Core.Repositories.Interfaces;

public interface ITenantSettingRepository
{
    Task<IEnumerable<TenantSetting>> GetAll();
    Task<TenantSetting> Create(TenantSetting entity);
}
