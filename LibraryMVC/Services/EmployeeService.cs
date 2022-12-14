using AutoMapper;
using LibraryMVC.Entities;
using LibraryMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryMVC.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly LibraryDbContext _dBContext;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public EmployeeService(LibraryDbContext dBContext, IMapper mapper, UserManager<User> userManager)
        {
            _dBContext = dBContext;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<List<UserDto>> GetAllAsync()
        {
            List<User> users = _dBContext
                .Users
                .AsNoTracking()
                .ToList();

            var usersDto = _mapper.Map<List<UserDto>>(users);

            foreach (var item in users)
            {
                var roleList = await _userManager.GetRolesAsync(item);
                var role = roleList[0];
                var itemId = item.Id;
                var singleUserDto = usersDto.FirstOrDefault(a => a.Id == itemId);
                singleUserDto.Role = role;
            }
            List<UserDto> listDto1 = new List<UserDto>();
            usersDto.Remove(usersDto.FirstOrDefault(a => a.Role == "Admin"));
            foreach(var item in usersDto)
            {
                if (item.Role == "Employee")
                    listDto1.Add(item);
            }
            foreach (var item in listDto1)
            {
                usersDto.Remove(item);
            }

            return usersDto;

        }

        public List<BorrowedBookDto> GetAllBooks(string id)
        {
            var user = _dBContext.Users.FirstOrDefault(a => a.Id == id);
            var currentUserName = user.UserName;
            var listOfBooks = _dBContext.BorrowedBooks.Where(a => a.UserName == currentUserName).ToList();
            var listOfBooksDto = _mapper.Map<List<BorrowedBookDto>>(listOfBooks);
            return listOfBooksDto;
        }

        public BorrowedBookDto Returning(int id)
        {
            var item = _dBContext.BorrowedBooks.FirstOrDefault(a => a.Id == id);
            var itemDto = _mapper.Map<BorrowedBookDto>(item);
            return itemDto;
        }

        public void ReturningConfirm(int id)
        {
            var borrowedBook = _dBContext.BorrowedBooks.FirstOrDefault(a => a.Id == id);
            var book = _dBContext.Books.FirstOrDefault(a => a.Id == borrowedBook.BookId);
            book.NumberOfPiecesAvailable = book.NumberOfPiecesAvailable + 1;
            _dBContext.BorrowedBooks.Remove(borrowedBook);
            _dBContext.Update(book);
            _dBContext.SaveChanges();
        }
    }
}
