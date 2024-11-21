using Microsoft.AspNetCore.Mvc;

namespace CompanyFleetManager.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UsersDatabaseAccess _dbAccess;

        public UsersController(UsersDatabaseAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }
    }
}
