namespace EpicRoadTrip.Application.Routes.CreateRoute;

public record CreateRouteRequest
{
    public double Distance { get; init; }

    public long Duration { get; init; }

    public int CityOneId { get; init; }

    public int CityTwoId { get; init; }

    public int RoadtripId { get; init; }
}