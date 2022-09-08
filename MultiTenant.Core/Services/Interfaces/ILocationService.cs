using MultiTenant.Core.Entities;

namespace MultiTenant.Core.Services.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetAll();
    }
}
