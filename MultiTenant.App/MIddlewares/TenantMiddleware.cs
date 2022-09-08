using MultiTenant.App.Features.Contexts;

namespace MultiTenant.App.MIddlewares
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IGlobalContext globalContext)
        {
            globalContext.SetContext(context);

            await _next(context);
        }
    }
}
