using MultiTenant.Core.Entities.Interfaces;

namespace MultiTenant.Core.Entities
{
    public class Location : EntityBase<int>, IMustHaveTenant
    {
        public string? TenantKey { get; set; }
        public string Address { get; set; }
    }
}
