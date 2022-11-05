using Microsoft.AspNetCore.Mvc;

namespace LibraryMVC.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
