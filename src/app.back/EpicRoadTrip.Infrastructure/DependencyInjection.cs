using EpicRoadTrip.Application.Authentications;
using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Application.Routes.GetRouteBetweenPoints.Bikes;
using EpicRoadTrip.Application.Users;
using EpicRoadTrip.Domain.External;
using EpicRoadTrip.Domain.HttpRequests;
using EpicRoadTrip.Infrastructure.Authentications;
using EpicRoadTrip.Infrastructure.Context;
using EpicRoadTrip.Infrastructure.Externals.Bike;
using EpicRoadTrip.Infrastructure.Externals.Configuration;
using EpicRoadTrip.Infrastructure.Externals.Train;
using EpicRoadTrip.Infrastructure.Options;
using EpicRoadTrip.Infrastructure.Repositories;
using EpicRoadTrip.Infrastructure.Routes;
using EpicRoadTrip.Infrastructure.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EpicRoadTrip.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);
        services.ConfigureRepositories();
    }

    private static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EpicRoadTripContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(nameof(ConnectionStrings.DefaultConnection))));
    }

    private static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IAuthenticationRepository), typeof(AuthenticationRepository));
        services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
        services.AddScoped(typeof(IExternalClientGet), typeof(TrainClientGet));
        services.AddScoped(typeof(IExternalRouteService), typeof(ExternalRouteService));
        services.AddHttpClient<TrainClientGet>();
        services.AddHttpClient<IHttpRequestService<IEnumerable<Domain.Routes.Route>, BikeParameters>, BikeHttpRequestService>(client =>
        {
            client.DefaultRequestHeaders.Add("X-Application-Id", "0c15f32b");
            client.DefaultRequestHeaders.Add("X-Api-Key", "8aaa43da725d1091021b5a94fa053c95");
            client.BaseAddress = new Uri(DataSources.BikeBaseAddress);
        });
    }
}