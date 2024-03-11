namespace EpicRoadTrip.Application.Roadtrips.GetRoadtrip;

public record GetRoadtripResponse
{
    public int Id { get; init; }

    public double Budget { get; init; }

    public DateTime StartDate { get; init; }

    public DateTime? EndDate { get; init; }
}