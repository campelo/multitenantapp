namespace MultiTenant.Core.Services;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;

    public LocationService(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public Task<Location> Create(Location entity)
    {
        return _locationRepository.Create(entity);
    }

    public Task<IEnumerable<Location>> GetAll()
    {
        return _locationRepository.GetAll();
    }
}
