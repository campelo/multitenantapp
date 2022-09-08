using MultiTenant.Core.Entities;

namespace MultiTenant.Core.Repositories.Interfaces
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAll();
    }
}
