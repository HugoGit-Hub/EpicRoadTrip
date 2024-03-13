namespace EpicRoadTrip.Application.Routes.UpdateRoute;

public record UpdateRouteRequest
{
    public int Id { get; init; }

    public double Distance { get; init; }

    public long Duration { get; init; }

    public int CityOneId { get; init; }

    public int CityTwoId { get; init; }

    public int RoadtripId { get; init; }
}