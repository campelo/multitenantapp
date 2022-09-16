namespace MultiTenant.Core.Entities
{
    /// <summary>
    /// A base tenant class
    /// </summary>
    public class Tenant : EntityBase<int>
    {
        /// <summary>
        /// Split character used on <see cref="GetTenantKey"/> and  <see cref="ParentTenantKey"/>
        /// </summary>
        public static readonly string TENANT_KEY_SPLITTER = ".";

        /// <summary>
        /// Tenant's constructor
        /// </summary>
        public Tenant()
            : this(null, null)
        {
        }

        /// <summary>
        /// Tenant's constructor
        /// </summary>
        /// <param name="tenantCode">Code used by the user or other apps to identity the tenant</param>
        /// <param name="tenantName">Tenant's full name</param>
        /// <param name="parentTenant">Tenant's parent</param>
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

        /// <summary>
        /// Immediate parent tenant's ID.
        /// </summary>
        public int? ParentTenantId { get; private set; }

        /// <summary>
        /// Immediate parent tenant.
        /// </summary>
        public Tenant? ParentTenant { get; private set; }

        /// <summary>
        /// Retrieve the immediate parent tenant key
        /// </summary>
        public string? ParentTenantKey { get; private set; }

        /// <summary>
        /// Retrieve the current tenant key
        /// </summary>
        public string GetTenantKey =>
            ParentTenantKey + $"{Id}{TENANT_KEY_SPLITTER}";

        /// <summary>
        /// Children tenants.
        /// </summary>
        public virtual IEnumerable<Tenant> Tenants { get; set; }

        /// <summary>
        /// Tenant settings.
        /// </summary>
        public virtual IEnumerable<TenantSetting> Settings { get; set; }
    }
}
