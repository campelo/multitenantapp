using MultiTenant.App.Features.TenantResolvers;

namespace MultiTenant.App.Features.Contexts
{
    public class GlobalContext : IGlobalContext
    {
        private readonly ITenantResolver _tenantResolver;

        public GlobalContext(ITenantResolver tenantResolver)
        {
            _tenantResolver = tenantResolver;
        }

        public string? TenantName { get; private set; }

        public void SetContext(HttpContext context)
        {
            TenantName = _tenantResolver.ResolveTenantName(context);
        }
    }
}
