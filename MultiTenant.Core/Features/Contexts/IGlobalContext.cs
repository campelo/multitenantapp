namespace MultiTenant.Core.Features.Contexts
{
    public interface IGlobalContext
    {
        public string? TenantKey { get; }

        void SetContext(string? tenantKey);
    }
}
