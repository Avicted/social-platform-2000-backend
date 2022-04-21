


using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace sp2000.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}