using LibraryMVC.Models;

namespace LibraryMVC.Services
{
    public interface IUserService
    {
        List<BorrowedBookDto> GetAll();
    }
}
