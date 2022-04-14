﻿using Infrastructure;
using Infrastructure.Persistance.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sp2000.Application.Interfaces;

namespace sp2000.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>();
        services.AddScoped<IRepositoryWrapper, RespositoryWrapper>();

        return services;
    }
}
