using EpicRoadTrip.Domain.Institutions;
using EpicRoadTrip.Domain.Roadtrips;

namespace EpicRoadTrip.Application.Institutions.GetInstitutionAround;

public record GetInstitutionAroundResponse
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public double? Price { get; init; }

    public string? PhoneNumber { get; init; }

    public string? Email { get; init; }

    public string? PreviewUrl { get; init; }

    public string Address { get; init; } = null!;

    public InstitutionType Type { get; init; }

    public string? WebSite { get; init; }

    public float Lat { get; init; }

    public float Lng { get; init; }
}