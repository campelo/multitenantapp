namespace MultiTenant.App.Features.Contexts
{
    public interface IGlobalContext
    {
        public string? TenantName { get; }

        void SetContext(HttpContext context);
    }
}
