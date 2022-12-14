using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryMVC.Entities;
using LibraryMVC.Services;
using LibraryMVC.Models;
using FluentValidation;
using FluentValidation.Results;
using System.Xml.Linq;
using LibraryMVC.Exceptions;
using LibraryMVC.Middleware;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace LibraryMVC.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IValidator<AddBookDto> _addValidator;
        private readonly IValidator<EditBookDto> _editValidator;

        public BookController(IBookService bookService, IValidator<AddBookDto> addValidator, IValidator<EditBookDto> editValidator)
        {
            _bookService = bookService;
            _addValidator = addValidator;
            _editValidator = editValidator;
        }

        // GET: Book
        public IActionResult Index()
        {
            var listOfBooksDto = _bookService.GetAll();
            return View(listOfBooksDto);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult Borrow(int id)
        {
            _bookService.Borrow(id);
            return RedirectToAction("Index", "User");
        }

        // GET: Book/Details/5
        public IActionResult Details(int id)
        {
            try
            {
                var bookDto = _bookService.GetBookDtoById(id);
                return View(bookDto);
            }
            catch(NotFoundException e)
            {
                return RedirectToAction("NotFoundExceptionView");
            }
        }

        // GET: Book/Create
        [Authorize(Roles = "Admin, Employee")]
        public IActionResult Create()
        {
            ViewBag.CategoryId = _bookService.SelectListItem();
            return View();
        }

        // POST: Book/Create
        [Authorize(Roles = "Admin, Employee")]
        [HttpPost]
        public  IActionResult Create(AddBookDto dto)
        {
            ViewBag.CategoryId = _bookService.SelectListItem();
            ValidationResult validResult =  _addValidator.Validate(dto);
            if (!validResult.IsValid)
            {
                return View("Create", dto);
            }
            _bookService.Add(dto);
            TempData["Success"] = "Book was added successfully";
            return RedirectToAction("Index");
        }

        // GET: Book/Edit/5
        [Authorize(Roles = "Admin, Employee")]
        public IActionResult Edit(int id)
        {
            ViewBag.CategoryId = _bookService.SelectListItem();
            try
            {
                var editBookDto = _bookService.GetBookDtoByIdEdit(id);
                return View(editBookDto);
            }
            catch
            {
                return RedirectToAction("NotFoundExceptionView");
            }
            
        }

        //POST: Book/Edit/5
        [Authorize(Roles = "Admin, Employee")]
        [HttpPost]
        public IActionResult Edit(EditBookDto dto)
        {
            ViewBag.CategoryId = _bookService.SelectListItem();
            ValidationResult validResult = _editValidator.Validate(dto);
            if (!validResult.IsValid)
            {
                return View("Edit", dto);
            }
            _bookService.Edit(dto);
            TempData["Success"] = "Book was edited successfully";
            return RedirectToAction("Index");
        }

        // GET: Book/Delete/5
        [Authorize(Roles = "Admin, Employee")]
        public IActionResult Delete(int id)
        {
            try
            {
                var bookDto = _bookService.GetBookDtoById(id);
                return View(bookDto);
            }
            catch(NotFoundException e)
            {
                return RedirectToAction("NotFoundExceptionView");
            }
        }

        // POST: Book/Delete/5
        [Authorize(Roles = "Admin, Employee")]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _bookService.Delete(id);
            TempData["Success"] = "Book was deleted successfully";
            return RedirectToAction("Index");
        }

        public IActionResult NotFoundExceptionView()
        {
            return View();
        }
    }
}
