namespace MultiTenant.App.Controllers;

[MultiTenantApiController]
public class LocationController
{
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet]
    public Task<IEnumerable<Location>> GetAll()
    {
        return _locationService.GetAll();
    }

    [HttpPost]
    public Task<Location> Create(Location location)
    {
        return _locationService.Create(location);
    }
}