namespace MultiTenant.Core.Entities.Interfaces;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}
