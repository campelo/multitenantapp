namespace MultiTenant.Core.Services.Interfaces;

public interface IServiceService
{
    Task<IEnumerable<Service>> GetAll();
    Task<Service> Create(Service service);
}
