using EpicRoadTrip.Domain.Routes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EpicRoadTrip.Infrastructure.Context.Configurations;

internal class RouteConfiguration : IEntityTypeConfiguration<Route>
{
    public void Configure(EntityTypeBuilder<Route> builder)
    {
        ConfigureRouteTable(builder);
    }

    private static void ConfigureRouteTable(EntityTypeBuilder<Route> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Distance);
        builder.Property(e => e.Duration);
        builder.Property(e => e.CityOneName);
        builder.Property(e => e.CityTwoName);
        builder.Property(e => e.RoadtripId);
        builder.OwnsOne(
            e => e.GeoJson,
            geoJson =>
            {
                geoJson.Property(e => e.Type);
                geoJson.OwnsMany(
                    e => e.Coordinates,
                    coordinate =>
                    {
                        coordinate.Property(e => e.Latitude);
                        coordinate.Property(e => e.Longitude);
                    });
            });
        builder.Property(e => e.RouteGroup);
        builder.Property(e => e.TransportType);
    }
}