using EpicRoadTrip.Domain.Institutions;
using EpicRoadTrip.Domain.Routes;

namespace EpicRoadTrip.Application.Roadtrips.GetAllRoadtripInformations;

public record GetRoadtripInformationsResponse
{
    public int Id { get; init; }

    public required string Title { get; init; }

    public double Budget { get; init; }

    public DateTime StartDate { get; init; }

    public DateTime? EndDate { get; init; }

    public TimeSpan Duration { get; init; }

    public int NbTransfers { get; init; }

    public IEnumerable<string>? Tags { get; init; }

    public string? Co2Emission { get; init; }

    public int UserId { get; init; }

    public ICollection<Route> Routes { get; init; } = [];

    public ICollection<Institution> Institutions { get; init; } = [];
}