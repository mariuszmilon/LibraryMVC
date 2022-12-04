using AutoMapper;
using LibraryMVC.Entities;
using LibraryMVC.Exceptions;
using LibraryMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryMVC.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly LibraryDbContext _dbContext;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, LibraryDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<Boolean> Login(LoginDto loginDto)
        {
            if (!_dbContext.Users.Any())
            {
                var adminUser = new User()
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    City = "gliwice",
                    Street = "kujawska",
                    StreetNumber = 2,
                    LastName = "admin",
                    PostalCode = "44-100"
                };
                var newUserResponse2 = await _userManager.CreateAsync(adminUser, "Admin123!");
                if (newUserResponse2.Succeeded) 
                {
                    await _userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
                }

            }

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginDto.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
                    if (result.Succeeded)
                    {
                        return true;
                    }
                }
                throw new WrongEmailOrPassword("Wrong email or password!");
            }
            return false;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Boolean> Register(RegisterDto registerDto)
        {
            var user = await _userManager.FindByEmailAsync(registerDto.Email);

            if(user != null)
                throw new WrongEmailOrPassword("This email address is already taken!");

            var newUser = _mapper.Map<User>(registerDto);
            var newUserResponse = await _userManager.CreateAsync(newUser, registerDto.Password);

            if (!newUserResponse.Succeeded)
                throw new FailedRegistration("Register is not completed!");

            await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            await _signInManager.SignInAsync(newUser, false);
            return true;
        }
    }
}
