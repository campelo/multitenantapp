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
                var t = new Tenant($"org{i}", $"Organization {i}");
                context.Tenants.Add(t);
                context.SaveChanges();
                if (i % 2 == 0)
                {
                    for (int k = 1; k <= 3; k++)
                    {
                        var subt = new Tenant($"sub{i}-{k}", $"Sub-organization {i}-{k}", t);
                        context.Tenants.Add(subt);
                        context.SaveChanges();
                        for (int j = 1; j <= 3; j++)
                        {
                            var l = new Location() { TenantKey = subt.GetTenantKey, Address = $"address location {subt.Code}.{j}", Name = $"location {subt.Code}.{j}", IsDeleted = false };
                            context.Locations.Add(l);
                        }
                    }
                }

                for (int j = 1; j <= 3; j++)
                {
                    var l = new Location() { TenantKey = t.GetTenantKey, Address = $"address location {i}.{j}", Name = $"location {i}.{j}", IsDeleted = false };
                    context.Locations.Add(l);
                }
            }
            context.SaveChanges();
        }
    }
}
