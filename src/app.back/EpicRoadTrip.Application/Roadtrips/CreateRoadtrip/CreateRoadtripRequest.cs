namespace EpicRoadTrip.Application.Roadtrips.CreateRoadtrip;

public record CreateRoadtripRequest
{
    public double Budget { get; init; }

    public DateTime StartDate { get; init; }

    public DateTime? EndDate { get; init; }
}