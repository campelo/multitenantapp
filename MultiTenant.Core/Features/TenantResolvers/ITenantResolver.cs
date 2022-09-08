using Microsoft.AspNetCore.Http;

namespace MultiTenant.Core.Features.TenantResolvers
{
    public interface ITenantResolver
    {
        Task<string?> ResolveTenantName(HttpContext context);
    }
}
