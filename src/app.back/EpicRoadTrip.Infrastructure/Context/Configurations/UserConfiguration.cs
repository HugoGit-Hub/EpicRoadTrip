using EpicRoadTrip.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EpicRoadTrip.Infrastructure.Context.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUserTable(builder);
    }

    private static void ConfigureUserTable(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.FirstName);
        builder.Property(e => e.LastName);
        builder.Property(e => e.Email);
        builder.Property(e => e.Password);
        builder.Property(e => e.Age);
        builder.Property(e => e.Gender);
    }
}