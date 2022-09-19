namespace MultiTenant.Repository.DbContexts;

/// <summary>
/// Multi-tenant DB context
/// </summary>
public class MultiTenantDbContext : DbContext, ITenant
{
    private readonly IGlobalContext _globalContext;

    /// <inheritdoc />
    public string? TenantKey { get => _globalContext.TenantKey; set { } }

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="options">Db context options</param>
    /// <param name="globalContext"><see cref="IGlobalContext"/> to handle tenant's data </param>
    public MultiTenantDbContext(DbContextOptions<MultiTenantDbContext> options, IGlobalContext globalContext) : base(options)
    {
        _globalContext = globalContext;
    }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Tenant> Tenants { get; set; }

    public virtual DbSet<TenantSetting> TenantSettings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // log SQL queries to console...
        optionsBuilder.LogTo(Console.WriteLine);
        base.OnConfiguring(optionsBuilder);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        this.ApplyMultitenantSavingRules(TenantKey);
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        this.ApplyMultitenantSavingRules(TenantKey);
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            List<Type> filterTypes = new();
            if (typeof(IMustHaveTenant).IsAssignableFrom(entityType.ClrType))
            {
                filterTypes.Add(typeof(IMustHaveTenant));
                entityType.ApplyTenantRules();
            }
            else if (typeof(IHasHierarchicalTenant).IsAssignableFrom(entityType.ClrType))
            {
                filterTypes.Add(typeof(IHasHierarchicalTenant));
                entityType.ApplyHierarchicalTenantRules();
            }
            else if (typeof(ISharedInTenant).IsAssignableFrom(entityType.ClrType))
            {
                filterTypes.Add(typeof(ISharedInTenant));
                entityType.ApplySharedInsideTenantRules();
            }
            if (typeof(IHasOptionsHandler).IsAssignableFrom(entityType.ClrType))
            {
                filterTypes.Add(typeof(IHasOptionsHandler));
                entityType.ApplyOptionsHandlerRules();
            }
            if (typeof(EntityBase<>).IsAssignableToGenericType(entityType.ClrType))
            {
                entityType.AddKey(entityType.GetProperty("Id"));
            }
            entityType.ApplyQueryFilter(this, filterTypes);
        }

        modelBuilder
            .Entity<Tenant>(entity =>
            {
                entity
                    .ToTable("Tenants")
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
            })
            .Entity<TenantSetting>(entity =>
            {
                entity
                    .ToTable("TenantSettings")
                    .HasOne(t => t.Tenant)
                    .WithMany(t => t.Settings)
                    .HasForeignKey(t => t.TenantId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity
                    .HasIndex(p => p.Key);
                entity
                    .HasKey(p => new { p.TenantId, p.Key });
            });

        base.OnModelCreating(modelBuilder);
    }
}
