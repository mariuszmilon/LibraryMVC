using LibraryMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMVC.Services
{
    public interface IAccountService
    {
        Task LogOut();
        Task<Boolean> Login(LoginDto loginDto);
        Task<Boolean> Register(RegisterDto registerDto);
    }
}
