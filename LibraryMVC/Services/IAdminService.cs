using LibraryMVC.Models;

namespace LibraryMVC.Services
{
    public interface IAdminService
    {
        Task<List<UserDto>> GetAllAsync();
        Task<RoleDto> GetRoleAsync(string id);
        Task EditRoleAsync(RoleDto dto);
        Task<UserDto> DeleteView(string id);
        Task<Boolean> DeleteConfirmed(string id);
    }
}
