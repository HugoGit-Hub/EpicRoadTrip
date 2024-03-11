namespace EpicRoadTrip.Application.Routes.GetRouteBetweenPoints;

public record GetRouteRequest
{
    public Tuple<float, float> CityOneCoord { get; } = null!;

    public Tuple<float, float> CityTwoCoord { get; } = null!;

    public IEnumerable<int> TransportationAllowedId { get; } = null!;
}
