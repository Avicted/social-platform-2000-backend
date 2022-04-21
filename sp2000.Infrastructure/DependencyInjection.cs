using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sp2000.Application.Interfaces;
using sp2000.Application.Models;
using sp2000.Infrastructure.Identity;
using sp2000.Infrastructure.Persistance.Repositories;
using sp2000.Infrastructure.Services;

namespace sp2000.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Database context
        services.AddDbContext<ApplicationDbContext>();
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();


        // Our Infrastructure services
        services.AddScoped<IRepositoryWrapper, RespositoryWrapper>();


        // Identity / Authentication & Authorization
        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

        services.AddAuthentication()
            .AddIdentityServerJwt();

        services.Configure<JwtBearerOptions>(
        IdentityServerJwtConstants.IdentityServerJwtBearerScheme,
        options =>
        {
            // ...
        });

        // Infrastructure service, external systems such as database, file system, network
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IIdentityService, IdentityService>();



        return services;
    }
}
