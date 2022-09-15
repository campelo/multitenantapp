namespace MultiTenant.App.Controllers;

public class ServiceController : MultiTenantController
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
    public Task<Service> Create(Service service)
    {
        return _serviceService.Create(service);
    }
}