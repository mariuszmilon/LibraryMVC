using FluentValidation;
using FluentValidation.Results;
using LibraryMVC.Entities;
using LibraryMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly LibraryDbContext _libraryDbContext;
        private readonly IValidator<LoginDto> _loginValidator;
        private readonly IValidator<RegisterDto> _registerValidator;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, LibraryDbContext libraryDbContext, IValidator<LoginDto> loginValidator, IValidator<RegisterDto> registerValidator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _libraryDbContext = libraryDbContext;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
        }

        public IActionResult Login()
        {
            var response = new LoginDto();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            ValidationResult validResult = _loginValidator.Validate(loginDto);

            if(!validResult.IsValid)
                return View(loginDto);

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if(user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginDto.Password);
                if(passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Book");
                    }
                }
                TempData["Error"] = "Wrong email or password!";
                return View(loginDto);
            }
            TempData["Error"] = "Wrong credentials. Please try again.";
            return View(loginDto);

        }
        public IActionResult Register()
        {
            var response = new RegisterDto();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            ValidationResult validResult = _registerValidator.Validate(registerDto);

            if (!validResult.IsValid)
                return View(registerDto);

            var user = _userManager.FindByEmailAsync(registerDto.Email);
            if(user.Result != null)
            {
                TempData["Error"] = "This email address is already taken!";
                return View(registerDto);
            }

            var newUser = new User()
            {
                UserName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerDto.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                TempData["Success"] = "Register completed!";
                return RedirectToAction("Index", "Book");
            }

            TempData["Error"] = "Register is not completed!";
            return View(registerDto);
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Book");
        }

    }
}
