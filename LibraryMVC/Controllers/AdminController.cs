using AutoMapper;
using FluentValidation;
using LibraryMVC.Entities;
using LibraryMVC.Models;
using LibraryMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FluentValidation.Results;

namespace LibraryMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IValidator<RoleDto> _roleValidator;

        public AdminController(IAdminService adminService, IValidator<RoleDto> roleValidator)
        {
            _adminService = adminService;
            _roleValidator = roleValidator;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var listofUsers = await _adminService.GetAllAsync();
            return View(listofUsers);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRole(string id)
        {
            var roleDto = await _adminService.GetRoleAsync(id);
            return View(roleDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRole(RoleDto dto)
        {
            ValidationResult validResult = _roleValidator.Validate(dto);
            if (!validResult.IsValid)
            {
                return View("EditRole", dto);
            }

            await _adminService.EditRoleAsync(dto);
            return RedirectToAction("Index");

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var userDto = await _adminService.DeleteView(id);
            return View(userDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            var result = await _adminService.DeleteConfirmed(id);
            if(result)
            {
                TempData["Success"] = "Successfully deleted!";
                return RedirectToAction("Index");
            }
            else
            TempData["Error"] = "Something went wrong!";
            return View();
        }


    }
}
