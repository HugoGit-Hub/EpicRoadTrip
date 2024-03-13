using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.ErrorHandling.Generics;
using EpicRoadTrip.Domain.Routes;
using EpicRoadTrip.Domain.Transportations;

namespace EpicRoadTrip.Domain.RouteTransportations;

public sealed class RouteTransportation
{
    public int RouteId { get; }

    public int TransportationId { get; }

    public Route Route { get; } = null!;

    public Transportation Transportation { get; } = null!;

    private RouteTransportation(int routeId, int transportationId)
    {
        RouteId = routeId;
        TransportationId = transportationId;
    }

    public static Result<RouteTransportation> Create(int routeId, int transportationId)
    {
        try
        {
            var routeTransportation = new RouteTransportation(routeId, transportationId);

            return Result<RouteTransportation>.Success(routeTransportation);
        }
        catch (Exception e)
        {
            return Result<RouteTransportation>.Failure(GenericErrors<RouteTransportation>.InvalidFormatError(e));
        }
    }
}