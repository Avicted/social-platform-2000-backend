using sp2000.Application.Models;

namespace sp2000.Application.Interfaces;
public interface IIdentityService
{
    Task<string> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<ApplicationUser> CreateUserAsync(string userName, string password);
}