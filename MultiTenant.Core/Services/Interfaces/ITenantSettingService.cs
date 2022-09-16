namespace MultiTenant.Core.Services.Interfaces;

public interface ITenantSettingService
{
    Task<IEnumerable<TenantSetting>> GetAll();
    Task<TenantSetting> Create(TenantSetting entity);
}
