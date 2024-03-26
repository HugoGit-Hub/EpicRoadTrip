namespace EpicRoadTrip.Application.Roadtrips.DeleteRoadtrip;

public record DeleteRoadtripResponse
{
    public int Id { get; init; }

    public required string Title { get; init; }

    public double Budget { get; init; }

    public DateTime StartDate { get; init; }

    public DateTime? EndDate { get; init; }
}