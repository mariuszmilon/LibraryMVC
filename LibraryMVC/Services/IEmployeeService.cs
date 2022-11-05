using LibraryMVC.Models;

namespace LibraryMVC.Services
{
    public interface IEmployeeService
    {
        Task<List<UserDto>> GetAllAsync();
    }
}
