using LibraryMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LibraryMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "User")]
        public IActionResult Index()
        {
            var listOfBooks = _userService.GetAll();
            return View(listOfBooks);
        }
    }
}
