namespace MultiTenant.Core.Services.Interfaces;

public interface ILocationService
{
    Task<IEnumerable<Location>> GetAll();
    Task<Location> Create(Location location);
}
