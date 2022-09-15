namespace MultiTenant.Core.Services;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _serviceRepository;

    public ServiceService(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public Task<Service> Create(Service service)
    {
        return _serviceRepository.Create(service);
    }

    public Task<IEnumerable<Service>> GetAll()
    {
        return _serviceRepository.GetAll();
    }
}
