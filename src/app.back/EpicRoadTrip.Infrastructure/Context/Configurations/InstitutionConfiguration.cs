using EpicRoadTrip.Domain.Institutions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EpicRoadTrip.Infrastructure.Context.Configurations;

internal class InstitutionConfiguration : IEntityTypeConfiguration<Institution>
{
    public void Configure(EntityTypeBuilder<Institution> builder)
    {
        ConfigureInstitutionTable(builder);
    }

    private static void ConfigureInstitutionTable(EntityTypeBuilder<Institution> builder)
    {
        builder.ToTable(nameof(Institution));
        builder.HasKey(institution => institution.Id);
        builder.Property(institution => institution.Name);
        builder.Property(institution => institution.Price);
        builder.Property(institution => institution.PhoneNumber);
        builder.Property(institution => institution.Email);
        builder.Property(institution => institution.Address);
        builder.Property(institution => institution.Type);
        builder.Property(institution => institution.CityId);
    }
}