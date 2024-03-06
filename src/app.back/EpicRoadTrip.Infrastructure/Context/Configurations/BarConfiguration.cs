using EpicRoadTrip.Domain.Bars;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EpicRoadTrip.Infrastructure.Context.Configurations;

internal class BarConfiguration : IEntityTypeConfiguration<Bar>
{
    public void Configure(EntityTypeBuilder<Bar> builder)
    {
        ConfigureBarTable(builder);
    }

    private static void ConfigureBarTable(EntityTypeBuilder<Bar> builder)
    {
        builder.ToTable("Bar");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name);
        builder.Property(b => b.Price);
        builder.Property(b => b.PhoneNumber);
        builder.Property(b => b.Email);
        builder.Property(b => b.Address);
        builder.Property(b => b.CityId);
    }
}