using Microsoft.AspNetCore.Mvc;

namespace CompanyFleetManager.Controllers
{
    [ApiController]
    [Route("api/fleet")]
    public class FleetController : ControllerBase
    {
        private readonly FleetDatabaseAccess _dbAccess;

        public FleetController(FleetDatabaseAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }
    }
}
