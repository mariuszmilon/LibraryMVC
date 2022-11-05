using AutoMapper;
using LibraryMVC.Entities;
using LibraryMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryMVC.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(IHttpContextAccessor httpContextAccessor, LibraryDbContext dbContext, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<BorrowedBookDto> GetAll()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User;
            var currentUserName = currentUser.Identity.Name;
            var listOfBooks = _dbContext.BorrowedBooks.Where(a => a.UserName == currentUserName).ToList();
            var listOfBooksDto = _mapper.Map<List<BorrowedBookDto>>(listOfBooks);
            return listOfBooksDto;
        }


    }
}
