namespace MultiTenant.Core.Entities.Interfaces;

/// <summary>
/// Using this interface, the item will be virtually deleted keeping it on data base but it will not be retrieved by normal searchs.
/// </summary>
public interface IHasOptionsHandler
{
    /// <summary>
    /// Flag to identify deleted, active items. <see cref="EntityOptions"/>
    /// </summary>
    EntityOptions Options { get; set; }
}
