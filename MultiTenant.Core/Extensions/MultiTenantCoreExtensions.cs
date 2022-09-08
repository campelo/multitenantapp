using Microsoft.Extensions.DependencyInjection;
using MultiTenant.Core.Services;
using MultiTenant.Core.Services.Interfaces;

namespace MultiTenant.Core.Extensions
{
    public static class MultiTenantCoreExtensions
    {
        public static IServiceCollection AddMultiTenantCore(this IServiceCollection services)
        {
            return services
                .AddTransient<ITenantService, TenantService>()
                .AddScoped<ILocationService, LocationService>();
        }
    }
}
