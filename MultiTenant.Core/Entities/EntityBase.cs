namespace MultiTenant.Core.Entities;

/// <summary>
/// This is a common base entity 
/// </summary>
/// <typeparam name="TId">Type of entity's ID</typeparam>
public abstract class EntityBase<TId> : IHasOptionsHandler
{
    public TId Id { get; set; }
    public string Name { get; set; }
    public EntityOptions Options { get; set; }
}
