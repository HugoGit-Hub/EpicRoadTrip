namespace EpicRoadTrip.Application.Routes.GetRoute;

public record GetRouteRequest
{
    public required Tuple<float, float> CityOneCoord { get; init; }

    public required Tuple<float, float> CityTwoCoord { get; init; }

    public required List<int> TransportationAllowedId { get; init; }
}
