namespace MultiTenant.Core.Repositories.Interfaces;

public interface ILocationRepository
{
    Task<IEnumerable<Location>> GetAll();
    Task<Location> Create(Location entity);
}
