namespace MultiTenant.App.Features.TenantResolvers
{
    public interface ITenantResolver
    {
        string? ResolveTenantName(HttpContext context);
    }
}
