using MultiTenant.App.Features.Contexts;
using MultiTenant.App.Features.TenantResolvers;
using MultiTenant.App.MIddlewares;
using MultiTenant.Core.Features.Contexts;
using MultiTenant.Core.Features.TenantResolvers;

namespace MultiTenant.App.Extensions
{
    public static class MultiTenantAppExtensions
    {
        public static IServiceCollection AddMultiTenant(this IServiceCollection services)
        {
            return services
                .AddScoped<IGlobalContext, GlobalContext>()
                .AddScoped<ITenantResolver, PathTenantResolver>();
        }
        public static IApplicationBuilder UseMultiTenant(this IApplicationBuilder builder)
        {
            return builder
                .UseMiddleware<TenantMiddleware>();
        }
    }
}
