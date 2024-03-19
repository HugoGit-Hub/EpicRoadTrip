using EpicRoadTrip.Domain.Institutions;
using EpicRoadTrip.Domain.Roadtrips;
using EpicRoadTrip.Domain.Routes;
using EpicRoadTrip.Domain.Transportations;
using EpicRoadTrip.Domain.Users;
using EpicRoadTrip.Infrastructure.Context.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EpicRoadTrip.Infrastructure.Context;

public class EpicRoadTripContext(DbContextOptions<EpicRoadTripContext> options) : DbContext(options)
{
    public DbSet<Institution> Institutions { get; set; } = null!;

    public DbSet<Roadtrip> Roadtrips { get; set; } = null!;

    public DbSet<Route> Routes { get; set; } = null!;

    public DbSet<Transportation> Transportations { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new InstitutionConfiguration().Configure(modelBuilder.Entity<Institution>());
        new RoadtripConfiguration().Configure(modelBuilder.Entity<Roadtrip>());
        new RouteConfiguration().Configure(modelBuilder.Entity<Route>());
        new TransportationConfiguration().Configure(modelBuilder.Entity<Transportation>());
        new UserConfiguration().Configure(modelBuilder.Entity<Domain.Users.User>());

        base.OnModelCreating(modelBuilder);
    }
}