using MultiTenant.Core.Entities;
using MultiTenant.Core.Repositories.Interfaces;
using MultiTenant.Core.Services.Interfaces;

namespace MultiTenant.Core.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public Task<IEnumerable<Location>> GetAll()
        {
            return _locationRepository.GetAll();
        }
    }
}
