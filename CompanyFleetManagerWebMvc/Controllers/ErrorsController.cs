using Microsoft.AspNetCore.Mvc;

namespace CompanyFleetManagerWebApp.Controllers
{
    public class ErrorsController : Controller
    {

        [Route("Errors/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return View("~/Views/Shared/Errors/404.cshtml");
                default:
                    return View("~/Views/Shared/Errors/GenericErrorStatusCode.cshtml", statusCode);
            }
        }
    }
}
