using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using sp2000.Application.Interfaces;
using sp2000.Infrastructure.Identity;
using sp2000.Infrastructure.Persistance;
using sp2000.Infrastructure.Persistance.Repositories;
using sp2000.Infrastructure.Services;

namespace sp2000.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Database context
        services.AddDbContext<ApplicationDbContext>();
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        // Our Infrastructure services
        services.AddScoped<IRepositoryWrapper, RespositoryWrapper>();


        services.AddAuthentication(options =>
        {
            // custom scheme defined in .AddPolicyScheme() below
            options.DefaultScheme = "JWT_OR_COOKIE";
            // options.DefaultChallengeScheme = "JWT_OR_COOKIE";
            options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie("Cookies", options =>
        {
            options.LoginPath = "/login";
            options.ExpireTimeSpan = TimeSpan.FromDays(1);
            options.Events.OnRedirectToLogin = ctx =>
            {
                if (ctx.Request.ContentType != null && ctx.Response.StatusCode == (int)HttpStatusCode.OK)
                {
                    if (ctx.Request.ContentType.Contains("application/json"))
                    {
                        ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    }
                }
                else
                {
                    ctx.Response.Redirect(ctx.RedirectUri);
                }

                return Task.CompletedTask;
            };
        })
        .AddJwtBearer("Bearer", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "http://localhost:8000",
                ValidateAudience = true,
                ValidAudience = "http://localhost:3000",
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hotdogornothotdog"))
            };
        })
        // this is the key piece!
        .AddPolicyScheme("JWT_OR_COOKIE", "JWT_OR_COOKIE", options =>
        {
            // runs on each request
            options.ForwardDefaultSelector = context =>
            {
                // filter by auth type
                string authorization = context.Request.Headers[HeaderNames.Authorization];
                if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
                    return "Bearer";

                // otherwise always check for cookie auth
                return "Cookies";
            };
        });

        // Example of how to customize a particular instance of cookie options and
        // is able to also use other services.
        services.AddSingleton<IConfigureOptions<CookieAuthenticationOptions>, ConfigureMyCookie>();



        // Infrastructure service, external systems such as database, file system, network
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IIdentityService, IdentityService>();

        return services;
    }
}
