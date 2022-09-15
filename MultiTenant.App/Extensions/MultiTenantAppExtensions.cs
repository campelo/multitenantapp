namespace MultiTenant.App.Extensions;

public static class MultiTenantAppExtensions
{
    /// <summary>
    /// Add multi-tenant services
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <returns>Service collection</returns>
    public static IServiceCollection AddMultiTenant(this IServiceCollection services)
    {
        return services
            .AddScoped<IGlobalContext, GlobalContext>()
            .AddScoped<ITenantResolver, PathTenantResolver>();
    }

    /// <summary>
    /// Apply and use multi-tenant features
    /// </summary>
    /// <param name="builder">Application builder</param>
    /// <returns>Application builder</returns>
    public static IApplicationBuilder UseMultiTenant(this IApplicationBuilder builder)
    {
        return builder
            .UseMiddleware<TenantMiddleware>();
    }
}
