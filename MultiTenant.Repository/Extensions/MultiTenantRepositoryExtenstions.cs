using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiTenant.Core.Repositories.Interfaces;
using MultiTenant.Repository.DbContexts;
using MultiTenant.Repository.Repositories;

namespace MultiTenant.Repository.Extensions
{
    public static class MultiTenantRepositoryExtenstions
    {
        public static IServiceCollection AddMultiTenantRepository(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddDbContext<MultiTenantDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("Default")))
                .AddTransient<ITenantRepository, TenantRepository>()
                .AddScoped<ILocationRepository, LocationRepository>();
        }

        public static void SeedDbContext(this IServiceProvider serviceProvider)
        {
            serviceProvider.SeedMultiTenantDbContext();
        }
    }
}
