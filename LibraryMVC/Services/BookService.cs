using AutoMapper;
using LibraryMVC.Entities;
using LibraryMVC.Exceptions;
using LibraryMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NuGet.Packaging.Signing;


namespace LibraryMVC.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;

        public BookService(LibraryDbContext dBContext, IMapper mapper, ILogger<BookService> logger, IWebHostEnvironment hostEnvironment)
        {
            _dbContext = dBContext;
            _mapper = mapper;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public void Add(AddBookDto dto)
        {
            _logger.LogInformation("Add function was invoked!");
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (dto.File != null && dto.File.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\books");
                var extension = Path.GetExtension(dto.File.FileName);
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    dto.File.CopyTo(fileStreams);
                }
                dto.ImageUrl = @"/images/books/" + fileName + extension;
            }
            var book = _mapper.Map<Book>(dto);
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }

        public SelectList SelectListItem()
        {
            _logger.LogInformation("SelectListItem function was invoked!");
            var selectListItems = new SelectList(_dbContext.Categories, "Id", "NameOfCategory");
            return selectListItems;
        }

        public List<BookDto> GetAll()
        {
            _logger.LogInformation("GetAll function was invoked!");
            var listOfBooks = _dbContext
                .Books
                .Include(b => b.Category)
                .AsNoTracking()
                .ToList();

            var listOfBooksDto = _mapper.Map<List<BookDto>>(listOfBooks);
            return listOfBooksDto;
        }

        public BookDto GetBookDtoById(int id)
        {
            _logger.LogInformation("GetBookDtoById function was invoked!");
            string wwwRootPath = _hostEnvironment.WebRootPath;
            var book = _dbContext
                .Books
                .Include(c => c.Category)
                .FirstOrDefault(a => a.Id == id);

            if (book is null)
                throw new NotFoundException($"Book with id: {id} not found!");

            var bookDto = _mapper.Map<BookDto>(book);
            return bookDto;
        }

        public EditBookDto GetBookDtoByIdEdit(int id)
        {
            _logger.LogInformation("GetBookDtoByIdEdit function was invoked!");
            var book = _dbContext
                .Books
                .Include(c => c.Category)
                .FirstOrDefault(a => a.Id == id);

            if (book is null)
                throw new NotFoundException($"Book with id: {id} not found!");

            var addBookDto = _mapper.Map<EditBookDto>(book);
            return addBookDto;
        }

        public void Edit(EditBookDto dto)
        {
            _logger.LogInformation("Edit function was invoked!");
            if(dto.File != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                var oldUrl = wwwRootPath + dto.ImageUrl;
                if (File.Exists(oldUrl))
                {
                    File.Delete(oldUrl);
                }
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\books");
                var extension = Path.GetExtension(dto.File.FileName);
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    dto.File.CopyTo(fileStreams);
                }
                dto.ImageUrl = @"/images/books/" + fileName + extension;
            }
            
            var book = _mapper.Map<Book>(dto);
            _dbContext.Books.Update(book);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _logger.LogInformation("Delete function was invoked!");
            var book = _dbContext
                .Books
                .FirstOrDefault(a => a.Id==id);

            if (book is null)
                throw new NotFoundException($"Book with id: {id} not found!");

            string wwwRootPath = _hostEnvironment.WebRootPath;
            var uploads = wwwRootPath + book.ImageUrl;
            if (File.Exists(uploads))
            {
                File.Delete(uploads);
            }

                _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}
