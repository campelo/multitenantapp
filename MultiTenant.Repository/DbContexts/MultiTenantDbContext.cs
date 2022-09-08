using Microsoft.EntityFrameworkCore;
using MultiTenant.Core.Entities;
using MultiTenant.Core.Entities.Interfaces;
using MultiTenant.Core.Features.Contexts;
using MultiTenant.Repository.Extensions;

namespace MultiTenant.Repository.DbContexts
{
    public class MultiTenantDbContext : DbContext, IMustHaveTenant
    {
        private readonly IGlobalContext _globalContext;

        public string? TenantId { get => _globalContext.TenantId; set { } }

        public MultiTenantDbContext(DbContextOptions<MultiTenantDbContext> options, IGlobalContext globalContext) : base(options)
        {
            _globalContext = globalContext;
        }

        public virtual DbSet<Location> Locations { get; set; }

        public virtual DbSet<Tenant> Tenants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // log SQL queries to console...
            //optionsBuilder.LogTo(Console.WriteLine);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //int tenantId = _globalContext.TenantId is null ? 0 : _globalContext.TenantId.Value;
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IMustHaveTenant).IsAssignableFrom(entityType.ClrType))
                {
                    entityType.AddTenantQueryFilter(this);
                }
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
