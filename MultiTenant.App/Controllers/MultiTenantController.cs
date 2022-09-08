using Microsoft.AspNetCore.Mvc;

namespace MultiTenant.App.Controllers
{
    [ApiController]
    [Route("api/{tenant}/[controller]/[action]")]
    public abstract class MultiTenantController : ControllerBase
    {
    }
}
