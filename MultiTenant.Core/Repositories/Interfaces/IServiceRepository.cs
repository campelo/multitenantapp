namespace MultiTenant.Core.Repositories.Interfaces;

public interface IServiceRepository
{
    Task<IEnumerable<Service>> GetAll();
    Task Delete(int id);
    Task<Service> Create(Service entity);
}
