using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using sp2000.Application.DTO;
using sp2000.Application.Helpers;
using sp2000.Application.Interfaces;
using sp2000.Application.Models;

namespace sp2000.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly IRepositoryWrapper _repository;
    private readonly ApplicationSettings _appSettings;
    private readonly IMapper _mapper;

    public IdentityService(
        IOptions<ApplicationSettings> appSettings,
        IRepositoryWrapper repository,
        IMapper mapper)
    {
        _appSettings = appSettings.Value;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ApplicationUserDto> GetUserByIdAsync(Guid userId)
    {
        var user = await _repository.ApplicationUser.GetUserByIdAsync(userId);

        if (user == null)
        {
            throw new ApiException(
                message: "User not found",
                statusCode: 404
            );
        }

        return _mapper.Map<ApplicationUserDto>(user);
    }

    public async Task<ApplicationUserDto> CreateUserAsync(RegisterNewUserDto user)
    {
        // validate
        int usersWithUsernameCount = _repository.ApplicationUser.FindByCondition(x => x.Username == user.Username).Count();

        if (usersWithUsernameCount > 0)
        {
            throw new ApiException(
                message: "Username '" + user.Username + "' is already taken"
            );
        }

        // create a new application user
        var newUser = new ApplicationUser()
        {
            Username = user.Username
        };

        // hash password
        newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

        // save user
        _repository.ApplicationUser.CreateUser(newUser);
        await _repository.SaveAsync();

        return _mapper.Map<ApplicationUserDto>(newUser);
    }

    public async Task<bool> DeleteUserAsync(ApplicationUser user)
    {
        // Retrieve entity by id
        var entity = await _repository.ApplicationUser.GetUserByIdAsync(user.Id);

        // Validate entity is not null
        if (entity != null)
        {
            _repository.ApplicationUser.DeleteUser(user);

            // Save changes to the database
            await _repository.SaveAsync();
        }

        return true;
    }

    public async Task<CustomApiResponse> LoginAsync(AuthenticateUserDto authenticateUser, HttpContext httpContext)
    {
        var user = _repository.ApplicationUser.FindByCondition(u => u.Username == authenticateUser.Username).FirstOrDefault();

        if (user == null)
        {
            return new CustomApiResponse(
                message: "Invalid login credentials",
                statusCode: 401
            );
        }

        var passwordHash = user.PasswordHash;
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(authenticateUser.Password, passwordHash);

        if (!isPasswordValid)
        {
            return new CustomApiResponse(
                message: "Invalid login credentials",
                statusCode: 401
            );
        }

        // create a new token with token helper and add our claim
        var token = JwtHelper.GetJwtToken(
            user.Username,
            "hotdogornothotdog",
            "http://localhost:8000",
            "http://localhost:3000",
            TimeSpan.FromMinutes(30),
            new[]
            {
                new Claim("UserState", user.ToString())
            });

        // also add cookie auth for Swagger Access
        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Username));
        identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));

        var principal = new ClaimsPrincipal(identity);

        await httpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(1)
            });

        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
        var expires = token.ValidTo;

        var response = new AuthenticationResponseDto(user, accessToken, expires);

        // We have a valid user login!
        return new CustomApiResponse(
            message: "Login successful!",
            statusCode: 200,
            result: response
        );
    }

    private string generateJwtToken(ApplicationUser user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}