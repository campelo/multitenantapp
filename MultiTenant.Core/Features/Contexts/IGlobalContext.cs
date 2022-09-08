namespace MultiTenant.Core.Features.Contexts
{
    public interface IGlobalContext
    {
        public string? TenantId { get; }

        void SetContext(string? tenantId);
    }
}
