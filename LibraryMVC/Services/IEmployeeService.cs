using LibraryMVC.Models;

namespace LibraryMVC.Services
{
    public interface IEmployeeService
    {
        Task<List<UserDto>> GetAllAsync();
        List<BorrowedBookDto> GetAllBooks(string id);
        BorrowedBookDto Returning(int id);
        void ReturningConfirm(int id);
    }
}
