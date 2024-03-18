namespace EpicRoadTrip.Application.Routes.UpdateRoute;

public record UpdateRouteRequest
{
    public int Id { get; init; }

    public double Distance { get; init; }

    public long Duration { get; init; }

    public required string CityOneName { get; init; }

    public required string CityTwoName { get; init; }

    public int RoadtripId { get; init; }
}