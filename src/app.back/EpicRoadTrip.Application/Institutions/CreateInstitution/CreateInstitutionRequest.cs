using EpicRoadTrip.Domain.Institutions;

namespace EpicRoadTrip.Application.Institutions.CreateInstitution;

public record CreateInstitutionRequest
{
    public required string Name { get; init; }

    public double? Price { get; init; }

    public string? PhoneNumber { get; init; }

    public string? Email { get; init; }

    public required string Address { get; init; }

    public InstitutionType Type { get; init; }

    public int RoadTripId { get; init; }

    public string? WebSite { get; init; }

    public required Tuple<float, float> Coord { get; init; }

}