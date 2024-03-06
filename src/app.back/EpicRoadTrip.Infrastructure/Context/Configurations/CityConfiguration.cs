using EpicRoadTrip.Domain.Cities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EpicRoadTrip.Infrastructure.Context.Configurations;

internal class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        ConfigureCityTable(builder);
    }

    private static void ConfigureCityTable(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id);
        builder.Property(c => c.Name).IsRequired();
    }
}