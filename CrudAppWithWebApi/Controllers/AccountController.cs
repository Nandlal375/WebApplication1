using Microsoft.AspNetCore.Mvc;

namespace CrudAppWithWebApi.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
