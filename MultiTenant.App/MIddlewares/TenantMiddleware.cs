using MultiTenant.Core.Features.Contexts;
using MultiTenant.Core.Features.TenantResolvers;

namespace MultiTenant.App.MIddlewares
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IGlobalContext globalContext, ITenantResolver tenantResolver)
        {
            string? tenantKey = await tenantResolver.ResolveTenantCode(context);
            globalContext.SetContext(tenantKey);

            await _next(context);
        }
    }
}
