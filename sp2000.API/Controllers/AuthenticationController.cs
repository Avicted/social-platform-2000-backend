using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using sp2000.Application.DTO;
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
    [HttpPost("register")]
    public async Task<CustomApiResponse> RegisterNewUser(RegisterNewUserDto user)
    {
        var result = await _identityService.CreateUserAsync(user);

        if (result == null)
        {
            return new CustomApiResponse(
                message: "Error the user could not be created",
                statusCode: 500
            );
        }


        return new CustomApiResponse(
            message: "User successfully created",
            statusCode: 201,
            result: result
        );
    }

    // Authenticate
    [HttpPost("login")]
    public async Task<CustomApiResponse> Authenticate(AuthenticateUserDto authenticateUser)
    {
        return await _identityService.LoginAsync(authenticateUser, HttpContext);
    }
}