namespace EpicRoadTrip.Application.Roadtrips.UpdateRoadtrip;

public record UpdateRoadtripRequest
{
    public int Id { get; init; }

    public required string Title { get; init; }

    public double Budget { get; init; }

    public DateTime StartDate { get; init; }

    public DateTime? EndDate { get; init; }

    public double Duration { get; init; }

    public int NbTransfers { get; init; }

    public IEnumerable<string>? Tags { get; init; }

    public string? Co2Emission { get; init; }
}