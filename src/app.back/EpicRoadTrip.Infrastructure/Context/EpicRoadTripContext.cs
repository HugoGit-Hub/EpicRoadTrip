using EpicRoadTrip.Domain.Bars;
using EpicRoadTrip.Domain.Cities;
using EpicRoadTrip.Domain.Events;
using EpicRoadTrip.Domain.Hotels;
using EpicRoadTrip.Domain.Restaurants;
using EpicRoadTrip.Domain.Roadtrips;
using EpicRoadTrip.Domain.Routes;
using EpicRoadTrip.Domain.RouteTransportations;
using EpicRoadTrip.Domain.Transportations;
using EpicRoadTrip.Infrastructure.Context.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EpicRoadTrip.Infrastructure.Context;

public class EpicRoadTripContext(DbContextOptions<EpicRoadTripContext> options) : DbContext(options)
{
    public DbSet<Bar> Bars { get; set; } = null!;

    public DbSet<City> Cities { get; set; } = null!;

    public DbSet<Event> Events { get; set; } = null!;

    public DbSet<Hotel> Hotels { get; set; } = null!;

    public DbSet<Restaurant> Restaurants { get; set; } = null!;

    public DbSet<Roadtrip> Roadtrips { get; set; } = null!;

    public DbSet<Route> Routes { get; set; } = null!;

    public DbSet<RouteTransportation> RouteTransportations { get; set; } = null!;

    public DbSet<Transportation> Transportations { get; set; } = null!;

    public DbSet<Domain.Users.User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new BarConfiguration().Configure(modelBuilder.Entity<Bar>());
        new CityConfiguration().Configure(modelBuilder.Entity<City>());
        new EventConfiguration().Configure(modelBuilder.Entity<Event>());
        new HotelConfiguration().Configure(modelBuilder.Entity<Hotel>());
        new RestaurantConfiguration().Configure(modelBuilder.Entity<Restaurant>());
        new RoadtripConfiguration().Configure(modelBuilder.Entity<Roadtrip>());
        new RouteConfiguration().Configure(modelBuilder.Entity<Route>());
        new RouteTransportationConfiguration().Configure(modelBuilder.Entity<RouteTransportation>());
        new TransportationConfiguration().Configure(modelBuilder.Entity<Transportation>());
        new UserConfiguration().Configure(modelBuilder.Entity<Domain.Users.User>());

        base.OnModelCreating(modelBuilder);
    }
}