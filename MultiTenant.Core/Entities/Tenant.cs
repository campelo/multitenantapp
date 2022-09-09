namespace MultiTenant.Core.Entities
{
    public class Tenant : EntityBase<int>
    {
        public Tenant()
        {
        }

        public Tenant(string tenantCode, string tenantName, Tenant parentTenant = null)
        {
            Code = tenantCode;
            Name = tenantName;
            ParentTenant = parentTenant;
            ParentTenantKey = parentTenant?.GetTenantKey;
        }

        /// <summary>
        /// Tenant identifier used for the client app to make calls to Backend
        /// </summary>
        public string Code { get; private set; }

        public int? ParentTenantId { get; private set; }
        
        /// <summary>
        /// Hierarchical tenants
        /// </summary>
        public Tenant? ParentTenant { get; private set; }

        /// <summary>
        /// Tenant key used to filter data
        /// </summary>
        public string? ParentTenantKey { get; private set; }

        public string GetTenantKey =>
            ParentTenantKey + $"{Id}.";

        public virtual IEnumerable<Tenant> Tenants { get; set; }
    }
}
