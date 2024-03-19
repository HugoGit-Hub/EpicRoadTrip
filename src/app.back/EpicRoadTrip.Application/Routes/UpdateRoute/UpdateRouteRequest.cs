using EpicRoadTrip.Domain.Routes;
using EpicRoadTrip.Domain.Transportations;

namespace EpicRoadTrip.Application.Routes.UpdateRoute;

public record UpdateRouteRequest
{
    public int Id { get; init; }

    public double Distance { get; init; }

    public long Duration { get; init; }

    public required string CityOneName { get; init; }

    public required string CityTwoName { get; init; }

    public required GeoJson GeoJson { get; init; }

    public Guid? RouteGroup { get; init; }

    public TransportationType TransportType { get; init; }

    public int RoadtripId { get; init; }
}