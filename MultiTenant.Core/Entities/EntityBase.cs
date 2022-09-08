namespace MultiTenant.Core.Entities
{
    public abstract class EntityBase<TId>
    {
        public TId Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
