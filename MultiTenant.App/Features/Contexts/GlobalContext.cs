using MultiTenant.Core.Features.Contexts;

namespace MultiTenant.App.Features.Contexts
{
    public class GlobalContext : IGlobalContext
    {
        public string? TenantKey { get; private set; }

        public void SetContext(string? tenantId)
        {
            TenantKey = tenantId;
        }
    }
}
