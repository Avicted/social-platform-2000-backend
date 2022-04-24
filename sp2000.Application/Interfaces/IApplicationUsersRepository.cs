using sp2000.Application.DTO;
using sp2000.Application.Helpers;
using sp2000.Application.Models;

namespace sp2000.Application.Interfaces;

public interface IApplicationUsersRepository : IRepositoryBase<ApplicationUser>
{
    Task<PagedList<ApplicationUserDto>> GetAllUsersAsync(ApplicationUserParameters applicationUserParameters);
    Task<ApplicationUser> GetUserByIdAsync(Guid id);
    void CreateUser(ApplicationUser user);
    void UpdateUser(ApplicationUser user);
    void DeleteUser(ApplicationUser user);
}