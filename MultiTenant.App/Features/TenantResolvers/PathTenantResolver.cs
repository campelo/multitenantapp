using MultiTenant.Core.Entities;
using MultiTenant.Core.Features.TenantResolvers;
using MultiTenant.Core.Services.Interfaces;

namespace MultiTenant.App.Features.TenantResolvers
{
    public class PathTenantResolver : ITenantResolver
    {
        private readonly ITenantService _tenantService;

        public PathTenantResolver(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        public async Task<string?> ResolveTenantName(HttpContext httpContext)
        {
            if (httpContext is null ||
                !httpContext.Request.RouteValues.Any(x => x.Key.Equals("tenant", StringComparison.OrdinalIgnoreCase)))
                return null;
            string? tenantName = httpContext.Request.RouteValues.First(x => x.Key.Equals("tenant", StringComparison.OrdinalIgnoreCase)).Value?.ToString();

            if (string.IsNullOrWhiteSpace(tenantName))
                return null;

            Tenant tenant = await _tenantService.GetByName(tenantName);

            return tenant?.Id ?? null;
        }
    }
}
