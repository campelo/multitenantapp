namespace MultiTenant.Core.Entities;

public class Location : EntityBase<int>, IHaveHierarchicalTenant
{
    public string TenantKey { get; set; }
    public string Address { get; set; }
}
