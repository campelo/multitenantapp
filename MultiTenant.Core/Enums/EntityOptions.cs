namespace MultiTenant.Core.Enums;

/// <summary>
/// Set entity status
/// </summary>
public enum EntityOptions
{
    /// <summary>
    /// An entity with this flag will not be retrieve by search query.
    /// </summary>
    Deleted = 0,

    /// <summary>
    /// An entity with this flag will be retrieve by search query.
    /// </summary>
    Active = 1
}
