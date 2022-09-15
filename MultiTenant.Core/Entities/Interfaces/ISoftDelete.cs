namespace MultiTenant.Core.Entities.Interfaces;

/// <summary>
/// Using this interface, the item will be virtually deleted keeping it on DataBase but it will not be retrieved by normal searchs.
/// </summary>
public interface ISoftDelete
{
    /// <summary>
    /// Flag to identify deleted items
    /// </summary>
    bool IsDeleted { get; set; }
}
