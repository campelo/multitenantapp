namespace MultiTenant.App.Attributes;

/// <summary>
/// Use this attribute to set your controller as api and multi-tenant default routes.
/// Use this attribute instead of <see cref="ApiControllerAttribute"/> and <see cref="RouteAttribute"/> on your API controllers.
/// </summary>
/// <remarks>
/// You can customize tenantCode router value by adding TenantIdentifier key with a value on your application config.
/// </remarks>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class MultiTenantApiControllerAttribute : ApiControllerAttribute, IRouteTemplateProvider
{
    /// <inheritdoc/>
    public string? Template => $"api/{{{AppConstantsSingleton.Instance.TenantIdentifier}}}/[controller]/[action]";

    /// <inheritdoc/>
    public int? Order { get; set; } = 0;

    /// <inheritdoc/>
    public string? Name { get; set; }
}
