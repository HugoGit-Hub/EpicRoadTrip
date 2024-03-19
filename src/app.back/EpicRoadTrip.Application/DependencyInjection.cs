using EpicRoadTrip.Application.Authentications;
using EpicRoadTrip.Application.CurrentUsers;
using EpicRoadTrip.Application.Options;
using EpicRoadTrip.Application.Roadtrips;
using EpicRoadTrip.Application.Routes;
using EpicRoadTrip.Domain.Authentications;
using EpicRoadTrip.Domain.CurrentUsers;
using EpicRoadTrip.Domain.Roadtrips;
using EpicRoadTrip.Domain.Routes;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace EpicRoadTrip.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMapster();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyMarker.Assembly));
        services.ConfigureServices();
    }
    
    private static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IRouteService, RouteService>();
        services.AddScoped<IRoadtripService, RoadtripService>();
    }
}