using EpicRoadTrip.Domain.Roadtrips;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EpicRoadTrip.Infrastructure.Context.Configurations;

internal class RoadtripConfiguration : IEntityTypeConfiguration<Roadtrip>
{
    public void Configure(EntityTypeBuilder<Roadtrip> builder)
    {
        ConfigureRoadtripTable(builder);
    }

    private static void ConfigureRoadtripTable(EntityTypeBuilder<Roadtrip> builder)
    {
        builder.ToTable("Roadtrip");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Budget);
        builder.Property(b => b.StartDate);
        builder.Property(b => b.EndDate);
        builder.Property(b => b.UserId);
    }
}