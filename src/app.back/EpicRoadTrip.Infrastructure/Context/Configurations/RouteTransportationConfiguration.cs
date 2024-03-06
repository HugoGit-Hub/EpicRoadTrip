using EpicRoadTrip.Domain.RouteTransportations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EpicRoadTrip.Infrastructure.Context.Configurations;

internal class RouteTransportationConfiguration : IEntityTypeConfiguration<RouteTransportation>
{
    public void Configure(EntityTypeBuilder<RouteTransportation> builder)
    {
        ConfigureRouteTransportationTable(builder);
    }

    private static void ConfigureRouteTransportationTable(EntityTypeBuilder<RouteTransportation> builder)
    {
        builder.HasNoKey();
        builder.Property(e => e.Cost);
        builder.Property(e => e.RouteId);
        builder.Property(e => e.TransportationId);
    }
}