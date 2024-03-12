namespace EpicRoadTrip.Application.Routes.GetRoute;

public record GetRouteResponse
{
    public int Id { get; }

    public double Distance { get; }

    public TimeSpan Duration { get; }

    public int CityOneId { get; }

    public int CityTwoId { get; }

    public int RoadtripId { get; }

    public string GeoJson { get; } = null!;
}
