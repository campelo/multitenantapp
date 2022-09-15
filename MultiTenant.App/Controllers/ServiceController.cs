namespace MultiTenant.App.Controllers;

[MultiTenantApiController]
public class ServiceController
{
    private readonly IServiceService _serviceService;

    public ServiceController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    [HttpGet]
    public Task<IEnumerable<Service>> GetAll()
    {
        return _serviceService.GetAll();
    }

    [HttpPost]
    public Task<Service> Create(ServiceDto dto)
    {
        Service service = new() { Name = dto.Name };
        return _serviceService.Create(service);
    }
}