using Microsoft.AspNetCore.Http;
using sp2000.Application.DTO;
using sp2000.Application.Helpers;

namespace sp2000.Application.Interfaces;
public interface IIdentityService
{
    Task<ApplicationUserDto> GetUserByIdAsync(Guid userId);

    Task<ApplicationUserDto> CreateUserAsync(RegisterNewUserDto user);

    Task<CustomApiResponse> LoginAsync(AuthenticateUserDto authenticateUser, HttpContext httpContext);
}