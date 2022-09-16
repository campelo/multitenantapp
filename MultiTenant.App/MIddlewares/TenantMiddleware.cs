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

    public async Task InvokeAsync(HttpContext context,
        IGlobalContext globalContext,
        ITenantResolver tenantResolver,
        ITenantSettingService tenantSettingService)
    {
        Tenant? currentTenant = await tenantResolver.ResolveTenantCode(context);
        await UseTenantInContext(globalContext, tenantSettingService, currentTenant);

        await _next(context);
    }

    private static async Task UseTenantInContext(IGlobalContext globalContext, ITenantSettingService tenantSettingService, Tenant? currentTenant)
    {
        if (currentTenant == null)
            return;
        globalContext.SetCurrentTenant(currentTenant);
        currentTenant.Settings = await tenantSettingService.GetAll();
        globalContext.SetCurrentTenant(currentTenant);
    }
}
