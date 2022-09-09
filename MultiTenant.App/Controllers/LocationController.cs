using Microsoft.AspNetCore.Mvc;
using MultiTenant.Core.Entities;
using MultiTenant.Core.Services.Interfaces;

namespace MultiTenant.App.Controllers
{
    public class LocationController : MultiTenantController
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public Task<IEnumerable<Location>> GetAll()
        {
            return _locationService.GetAll();
        }

        [HttpPost]
        public Task<Location> Create(Location location)
        {
            return _locationService.Create(location);
        }
    }
}