using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using sp2000.Application.DTO;
using sp2000.Application.Models;
using sp2000.Application.Helpers;
using sp2000.Application.Interfaces;

namespace sp2000.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public AuthenticationController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    // Register a new user
    [HttpPost("/register")]
    public async Task<CustomApiResponse> RegisterNewUser(RegisterNewUserDto data)
    {
        return await _identityService.CreateUserAsync(data.Username, data.Password);
    }

    // Authenticate
    /* [HttpPost("/login")]
    public async Task<CustomApiResponse> Authenticate(AuthenticateUserDto authenticateUser)
    {
        // return await _identityService.AuthorizeAsync()
    } */
}