using AutoMapper;
using LibraryMVC.Entities;
using LibraryMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibraryMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly LibraryDbContext _dBContext;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AdminController(LibraryDbContext dBContext, UserManager<User> userManager, IMapper mapper)
        {
            _dBContext = dBContext;
            _userManager = userManager;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            List<User> users = _dBContext
                .Users
                .AsNoTracking()
                .ToList();

            var usersDto = _mapper.Map<List<UserDto>>(users);

            foreach(var item in users)
            {
                var roleList = await _userManager.GetRolesAsync(item);
                var role = roleList[0];
                var itemId = item.Id;
                var singleUserDto = usersDto.FirstOrDefault(a => a.Id == itemId);
                singleUserDto.Role = role;
            }
            usersDto.Remove(usersDto.FirstOrDefault(a => a.Role == "Admin"));

            return View(usersDto);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            RoleDto roleDto = new RoleDto() { Id = id};
            var roleList = await _userManager.GetRolesAsync(user);
            roleDto.Role = roleList[0];
            return View(roleDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRole(RoleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", dto);
            }
            var userRole = _dBContext
                .Users
                .FirstOrDefault(a => a.Id == dto.Id);
            var userRoleId = _dBContext
                .UserRoles
                .FirstOrDefault(a => a.UserId == userRole.Id);

            var userRoleString = _dBContext
                .Roles
                .FirstOrDefault(a => a.Id == userRoleId.RoleId);
            await _userManager.RemoveFromRoleAsync(userRole, userRoleString.Name);
            await _userManager.AddToRoleAsync(userRole, dto.Role);
            return RedirectToAction("Index");

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userDto = _mapper.Map<UserDto>(user);
            var role = await _userManager.GetRolesAsync(user);
            userDto.Role = role[0];
            return View(userDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["Success"] = "Successfully deleted!";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Something went wrong!";
            return View();
        }


    }
}
