using EpicRoadTrip.Application.Authentications;
using EpicRoadTrip.Application.Options;
using EpicRoadTrip.Application.Users;
using EpicRoadTrip.Domain.Authentications;
using EpicRoadTrip.Domain.Users;
using Microsoft.Extensions.DependencyInjection;

namespace EpicRoadTrip.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyMarker.Assembly));
        services.ConfigureServices();
    }
    
    private static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserService, UserService>();
    }
}