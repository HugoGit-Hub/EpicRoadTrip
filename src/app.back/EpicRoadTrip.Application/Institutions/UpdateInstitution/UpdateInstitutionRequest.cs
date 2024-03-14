using EpicRoadTrip.Domain.Institutions;

namespace EpicRoadTrip.Application.Institutions.UpdateInstitution;

public record UpdateInstitutionRequest
{
    public int Id { get; init; }

    public required string Name { get; init; }

    public double? Price { get; init; }

    public string? PhoneNumber { get; init; }

    public string? Email { get; init; }

    public required string Address { get; init; }

    public InstitutionType Type { get; init; }

    public int CityId { get; init; }
}