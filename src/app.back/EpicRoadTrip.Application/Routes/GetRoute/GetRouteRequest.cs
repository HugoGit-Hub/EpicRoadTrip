namespace EpicRoadTrip.Application.Routes.GetRoute;

public record GetRouteRequest
{
    public Tuple<float, float> CityOneCoord { get; } = null!;

    public Tuple<float, float> CityTwoCoord { get; } = null!;

    public IEnumerable<int> TransportationAllowedId { get; } = null!;
}
