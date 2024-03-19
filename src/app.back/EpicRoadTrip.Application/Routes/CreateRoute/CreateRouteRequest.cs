using EpicRoadTrip.Domain.Transportations;

namespace EpicRoadTrip.Application.Routes.CreateRoute;

public record CreateRouteRequest
{
    public double Distance { get; init; }

    public long Duration { get; init; }

    public required string CityOneName { get; init; }

    public required string CityTwoName { get; init; }

    public Guid? RouteGroup { get; init; }

    public TransportationType TransportType { get; init; }

    public int RoadtripId { get; init; }

    public required string GeoJson { get; init; }
}