namespace MultiTenant.Repository.DbContexts;

public class MultiTenantDbContext : DbContext, ITenant
{
    private readonly IGlobalContext _globalContext;

    public string TenantKey { get => _globalContext.TenantKey; set { } }

    public MultiTenantDbContext(DbContextOptions<MultiTenantDbContext> options, IGlobalContext globalContext) : base(options)
    {
        _globalContext = globalContext;
    }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Tenant> Tenants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // log SQL queries to console...
        optionsBuilder.LogTo(Console.WriteLine);
        base.OnConfiguring(optionsBuilder);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        this.AddTenantIfNeeded(TenantKey);
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        this.AddTenantIfNeeded(TenantKey);
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(IMustHaveTenant).IsAssignableFrom(entityType.ClrType))
            {
                entityType.AddTenantQueryFilter(this);
            }
            if (typeof(ISharedInTenant).IsAssignableFrom(entityType.ClrType))
            {
                entityType.AddSharedInsideTenantQueryFilter(this);
            }
            if (typeof(EntityBase<>).IsAssignableToGenericType(entityType.ClrType))
            {
                entityType.AddKey(entityType.GetProperty("Id"));
            }
        }

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity
                .HasOne(t => t.ParentTenant)
                .WithMany(t => t.Tenants)
                .HasForeignKey(t => t.ParentTenantId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .Property(p => p.Code)
                .IsUnicode(false)
                .HasMaxLength(RepositoryContants.HIERACHYCAL_TENANT_ID_LENGTH);
            entity
                .HasIndex(p => p.Code);
            entity
                .HasAlternateKey(p => p.Code);
        });

        base.OnModelCreating(modelBuilder);
    }
}
