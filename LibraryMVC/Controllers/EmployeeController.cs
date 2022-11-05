using LibraryMVC.Models;
using LibraryMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [Authorize(Roles = "Employee")]
        public IActionResult Books(string id)
        {

            var list = _employeeService.GetAllBooks(id);
            return View(list);
        }

        [Authorize(Roles = "Employee")]
        public IActionResult Returning(int id)
        {
            var returnDto = _employeeService.Returning(id);
            return View(returnDto);
        }

        [HttpPost, ActionName("Returning")]
        [Authorize(Roles = "Employee")]
        public IActionResult ReturningConfirm(int id)
        {
            _employeeService.ReturningConfirm(id);
            return RedirectToAction("Index");
        }

    }
}
