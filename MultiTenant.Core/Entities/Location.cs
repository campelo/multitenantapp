﻿namespace MultiTenant.Core.Entities;

public class Location : EntityBase<int>, IHasHierarchicalTenant
{
    public string TenantKey { get; set; }
    public string Address { get; set; }
}
