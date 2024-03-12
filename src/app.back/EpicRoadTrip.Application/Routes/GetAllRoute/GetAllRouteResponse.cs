namespace EpicRoadTrip.Application.Routes.GetAllRoute;

public record GetAllRouteResponse
{
    public int Id { get; init; }

    public double Distance { get; init; }

    public TimeSpan Duration { get; init; }

    public int CityOneId { get; init; }

    public int CityTwoId { get; init; }

    public int RoadtripId { get; init; }
}