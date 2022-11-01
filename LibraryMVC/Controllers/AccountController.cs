using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using LibraryMVC.Entities;
using LibraryMVC.Exceptions;
using LibraryMVC.Models;
using LibraryMVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IValidator<LoginDto> _loginValidator;
        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly IAccountService _accountService;

        public AccountController(IValidator<LoginDto> loginValidator, IValidator<RegisterDto> registerValidator, IAccountService accountService)
        {
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
            _accountService = accountService;
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

            try
            {
                var user = await _accountService.Login(loginDto);
                if (user)
                    return RedirectToAction("Index", "Book");
            }
            catch(WrongEmailOrPassword e)
            {
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

            try
            {
                var result = await _accountService.Register(registerDto);
                if (result)
                {
                    TempData["Success"] = "Register completed!";
                    return RedirectToAction("Index", "Book");
                }
            }
            catch(WrongEmailOrPassword e)
            {
                TempData["Error"] = "This email address is already taken!";
                return View(registerDto);
            }
            catch(FailedRegistration e)
            {
                TempData["Error"] = "Register is not completed!";
                return View(registerDto);
            }
            return View(registerDto);
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _accountService.LogOut();
            return RedirectToAction("Index", "Book");
        }

    }
}
