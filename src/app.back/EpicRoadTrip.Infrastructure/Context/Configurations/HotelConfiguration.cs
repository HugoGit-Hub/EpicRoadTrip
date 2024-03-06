using EpicRoadTrip.Domain.Hotels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EpicRoadTrip.Infrastructure.Context.Configurations;

internal class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        ConfigureHotelTable(builder);
    }

    private static void ConfigureHotelTable(EntityTypeBuilder<Hotel> builder)
    {
        builder.ToTable("Hotel");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name);
        builder.Property(b => b.Price);
        builder.Property(b => b.PhoneNumber);
        builder.Property(b => b.Email);
        builder.Property(b => b.Address);
        builder.Property(b => b.CityId);
    }
}