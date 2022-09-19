namespace MultiTenant.Core.Services;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _serviceRepository;

    public ServiceService(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public Task<Service> Create(Service entity)
    {
        return _serviceRepository.Create(entity);
    }

    public Task Delete(int id)
    {
        return _serviceRepository.Delete(id);
    }

    public Task<IEnumerable<Service>> GetAll()
    {
        return _serviceRepository.GetAll();
    }
}
