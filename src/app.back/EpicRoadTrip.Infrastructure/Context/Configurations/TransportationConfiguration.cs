using EpicRoadTrip.Domain.Transportations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EpicRoadTrip.Infrastructure.Context.Configurations;

internal class TransportationConfiguration : IEntityTypeConfiguration<Transportation>
{
    public void Configure(EntityTypeBuilder<Transportation> builder)
    {
        ConfigureTransportationTable(builder);
    }

    private static void ConfigureTransportationTable(EntityTypeBuilder<Transportation> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Score);
        builder.Property(e => e.Company);
        builder.Property(e => e.Address);
        builder.Property(e => e.TransportationType);
    }
}