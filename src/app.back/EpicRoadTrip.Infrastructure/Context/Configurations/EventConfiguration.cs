using EpicRoadTrip.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EpicRoadTrip.Infrastructure.Context.Configurations;

internal class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        ConfigureEventTable(builder);
    }

    private static void ConfigureEventTable(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("Event");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name);
        builder.Property(b => b.Price);
        builder.Property(b => b.PhoneNumber);
        builder.Property(b => b.Email);
        builder.Property(b => b.Address);
        builder.Property(b => b.CityId);
    }
}