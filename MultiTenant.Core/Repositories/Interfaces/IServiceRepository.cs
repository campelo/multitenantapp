namespace MultiTenant.Core.Repositories.Interfaces;

public interface IServiceRepository
{
    Task<IEnumerable<Service>> GetAll();
    Task<Service> Create(Service location);
}
