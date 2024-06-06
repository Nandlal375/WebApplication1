using Microsoft.AspNetCore.Mvc;

namespace CrudAppWithWebApi.Controllers
{
    [Route("Error/{statusCode}")]
    public class ErrorController : Controller
    {
        public IActionResult Index(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.Errmsg = "Page Not Found";
                    break;
                default:
                    break;
            }
            return View("NotFound");
        }
    }
}
