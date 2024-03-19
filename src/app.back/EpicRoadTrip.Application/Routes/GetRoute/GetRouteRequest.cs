using EpicRoadTrip.Domain.Transportations;

namespace EpicRoadTrip.Application.Routes.GetRoute;

public record GetRouteRequest
{
    public required Tuple<double, double> CityOneCoord { get; init; }

    public required Tuple<double, double> CityTwoCoord { get; init; }

    public required List<TransportationType> TransportationAllowedId { get; init; }
}
