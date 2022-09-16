namespace MultiTenant.App.Features.Contexts;

/// <inheritdoc />
public class GlobalContext : IGlobalContext
{
    private Tenant? _currentTenant;

    /// <inheritdoc />
    public string? TenantKey => _currentTenant?.GetTenantKey;

    /// <inheritdoc />
    public void SetCurrentTenant(Tenant? currentTenant)
    {
        _currentTenant = currentTenant;
    }
}
