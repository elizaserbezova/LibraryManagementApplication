using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApplication.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HandleErrorCode(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return View("404");
            }
            return View("GenericError");
        }

        [Route("Error/500")]
        public IActionResult HandleServerError()
        {
            return View("500");
        }
    }
}
