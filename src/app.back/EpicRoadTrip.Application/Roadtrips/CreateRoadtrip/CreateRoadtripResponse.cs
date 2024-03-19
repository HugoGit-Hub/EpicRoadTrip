namespace EpicRoadTrip.Application.Roadtrips.CreateRoadtrip;

public record CreateRoadtripResponse
{
    public int Id { get; init; }

    public double Budget { get; init; }

    public DateTime StartDate { get; init; }

    public DateTime? EndDate { get; init; }

    public TimeSpan Duration { get; init; }

    public int NbTransfers { get; init; }

    public IEnumerable<string>? Tags { get; init; }

    public string? Co2Emission { get; init; }
}