using sp2000.Application.Models;
using sp2000.Application.Interfaces;
using sp2000.Application.Helpers;
using sp2000.Application.DTO;
using Microsoft.EntityFrameworkCore;

namespace sp2000.Infrastructure.Persistance.Repositories;

public class ApplicationUsersRepository : RepositoryBase<ApplicationUser>, IApplicationUsersRepository
{
    public ApplicationUsersRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateUser(ApplicationUser user)
    {
        Create(user);
    }

    public void DeleteUser(ApplicationUser user)
    {
        Delete(user);
    }
    public async Task<PagedList<ApplicationUserDto>> GetAllUsersAsync(ApplicationUserParameters applicationUserParameters)
    {
        var source = FindAll()
             .OrderByDescending(u => u.CreatedDate)
             .Select(u => new ApplicationUserDto
             {

                 Username = u.Username
             });

        return await PagedList<ApplicationUserDto>.ToPagedListAsync(source, applicationUserParameters.PageNumber, applicationUserParameters.PageSize);
    }

    public async Task<ApplicationUser> GetUserByIdAsync(Guid id)
    {
        return await FindByCondition(u => u.Id == id)
            .FirstOrDefaultAsync();
    }

    public void UpdateUser(ApplicationUser user)
    {
        Update(user);
    }
}