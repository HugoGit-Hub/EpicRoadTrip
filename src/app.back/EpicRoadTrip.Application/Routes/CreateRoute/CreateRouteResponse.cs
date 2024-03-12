namespace EpicRoadTrip.Application.Routes.CreateRoute;

public record CreateRouteResponse
{
    public int Id { get; init; }

    public double Distance { get; init; }

    public TimeSpan Duration { get; init; }

    public int CityOneId { get; init; }

    public int CityTwoId { get; init; }

    public int RoadtripId { get; init; }
}