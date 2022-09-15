namespace MultiTenant.App.Features.Contexts;

/// <inheritdoc />
public class GlobalContext : IGlobalContext
{
    /// <inheritdoc />
    public string? TenantKey { get; private set; }

    /// <inheritdoc />
    public void SetContext(string? tenantId)
    {
        TenantKey = tenantId;
    }
}
