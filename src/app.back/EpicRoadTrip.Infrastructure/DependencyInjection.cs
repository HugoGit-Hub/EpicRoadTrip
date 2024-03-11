﻿using EpicRoadTrip.Application.Authentications;
using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Application.Routes;
using EpicRoadTrip.Application.Users;
using EpicRoadTrip.Infrastructure.Authentications;
using EpicRoadTrip.Infrastructure.Context;
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
        services.AddScoped(typeof(IRouteRepository), typeof(RouteRepository));
    }
}