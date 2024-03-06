using EpicRoadTrip.Domain.Restaurants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EpicRoadTrip.Infrastructure.Context.Configurations;

internal class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        ConfigureRestaurantTable(builder);
    }

    private static void ConfigureRestaurantTable(EntityTypeBuilder<Restaurant> builder)
    {
        builder.ToTable("Restaurant");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name);
        builder.Property(b => b.Price);
        builder.Property(b => b.PhoneNumber);
        builder.Property(b => b.Email);
        builder.Property(b => b.Address);
        builder.Property(b => b.CityId);
    }
}