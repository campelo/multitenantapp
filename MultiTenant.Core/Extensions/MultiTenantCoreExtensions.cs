namespace MultiTenant.Core.Extensions;

public static class MultiTenantCoreExtensions
{
    public static IServiceCollection AddMultiTenantCore(this IServiceCollection services)
    {
        return services
            .AddTransient<ITenantService, TenantService>()
            .AddScoped<ILocationService, LocationService>()
            .AddScoped<IServiceService, ServiceService>();
    }
}
