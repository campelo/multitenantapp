namespace MultiTenant.Core.Entities;

public class Service : EntityBase<int>, ISharedInTenant
{
    public string TenantKey { get; set; }
}
