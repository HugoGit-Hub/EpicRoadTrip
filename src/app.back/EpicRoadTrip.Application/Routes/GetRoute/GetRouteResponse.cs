namespace EpicRoadTrip.Application.Routes.GetRoute;

public record GetRouteResponse
{
    public int Id { get; init; }

    public double Distance { get; init; }

    public TimeSpan Duration { get; init; }

    public required string CityOneName { get; init; }

    public required string CityTwoName { get; init; }

    public int RoadtripId { get; init; }

    public required string GeoJson { get; init; }
}
