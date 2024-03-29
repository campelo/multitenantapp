﻿namespace MultiTenant.Repository.Extensions;

public static class MultiTenantRepositoryExtenstions
{
    public static IServiceCollection AddMultiTenantRepository(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext<MultiTenantDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default")))
            .AddScoped<ITenantRepository, TenantRepository>()
            .AddScoped<ITenantSettingRepository, TenantSettingRepository>()
            .AddScoped<ILocationRepository, LocationRepository>()
            .AddScoped<IServiceRepository, ServiceRepository>();
    }

    public static void SeedDbContext(this IServiceProvider serviceProvider)
    {
        serviceProvider.SeedMultiTenantDbContext();
    }
}
