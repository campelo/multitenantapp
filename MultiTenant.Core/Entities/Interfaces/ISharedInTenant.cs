﻿namespace MultiTenant.Core.Entities.Interfaces;

/// <summary>
/// An entity implementing this interface could be seen by the main tenant and all their children...
/// </summary>
public interface ISharedInTenant : ITenant
{
    /// <inheritdoc />
    new string TenantKey { get; set; }
}
