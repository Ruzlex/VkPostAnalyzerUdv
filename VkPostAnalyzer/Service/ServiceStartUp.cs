using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;

namespace Service;

public static class ServiceStartUp
{
    public static IServiceCollection AddServces(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPostAnalyzeService, PostAnalyzeService>();

        return services;
    }
}