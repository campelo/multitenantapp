namespace MultiTenant.App.MIddlewares;

/// <summary>
/// This middleware will intercept all the requests to retrieve tenant information.
/// </summary>
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
