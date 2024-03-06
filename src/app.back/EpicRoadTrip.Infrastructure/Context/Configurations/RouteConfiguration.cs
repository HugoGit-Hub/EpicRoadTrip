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
        builder.Property(e => e.CityOneId);
        builder.Property(e => e.CityTwoId);
        builder.Property(e => e.RoadtripId);
    }
}