using Microsoft.Extensions.DependencyInjection;
using MultiTenant.Core.Entities;

namespace MultiTenant.Repository.DbContexts
{
    public static class SeedDbContext
    {
        public static void SeedMultiTenantDbContext(this IServiceProvider service)
        {
            using var scope = service.CreateScope();
            using MultiTenantDbContext context = scope.ServiceProvider.GetRequiredService<MultiTenantDbContext>();
            context.Database.EnsureCreated();
            if (context.Tenants.Any())
                return;
            for (int i = 1; i <= 3; i++)
            {
                var t = new Tenant() { Id = i.ToString(), Name = $"org{i}", IsDeleted = false };
                context.Tenants.Add(t);

                for (int j = 1; j <= 3; j++)
                {
                    var l = new Location() { Address = $"address location {i}.{j}", Name = $"location {i}.{j}", TenantId = i.ToString(), IsDeleted = false };
                    context.Locations.Add(l);
                }
            }
            context.SaveChanges();
        }
    }
}
