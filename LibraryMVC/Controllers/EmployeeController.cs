using LibraryMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LibraryMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Index()
        {
            var listofUsers = await _employeeService.GetAllAsync();
            return View(listofUsers);
        }
    }
}
