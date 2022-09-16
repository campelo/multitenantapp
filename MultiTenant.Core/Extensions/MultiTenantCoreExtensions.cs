namespace MultiTenant.Core.Extensions;

public static class MultiTenantCoreExtensions
{
    public static IServiceCollection AddMultiTenantCore(this IServiceCollection services)
    {
        return services
            .AddScoped<ITenantService, TenantService>()
            .AddScoped<ITenantSettingService, TenantSettingService>()
            .AddScoped<ILocationService, LocationService>()
            .AddScoped<IServiceService, ServiceService>();
    }
}
