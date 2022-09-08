namespace MultiTenant.App.Features.TenantResolvers
{
    public class PathTenantResolver : ITenantResolver
    {
        public string? ResolveTenantName(HttpContext httpContext)
        {
            if (httpContext is null ||
                !httpContext.Request.RouteValues.Any(x => x.Key.Equals("tenant", StringComparison.OrdinalIgnoreCase)))
                return null;
            string? tenantName = httpContext.Request.RouteValues.First(x => x.Key.Equals("tenant", StringComparison.OrdinalIgnoreCase)).Value?.ToString();
            return tenantName;
        }
    }
}
