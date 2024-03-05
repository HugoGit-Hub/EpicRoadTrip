using EpicRoadTrip.Domain.Routes;
using EpicRoadTrip.Domain.RouteTransportations.Exceptions;
using EpicRoadTrip.Domain.Transportations;

namespace EpicRoadTrip.Domain.RouteTransportations;

public sealed class RouteTransportation
{
    public double Cost { get; }

    public int RouteId { get; }

    public int TransportationId { get; }

    public Route Route { get; } = null!;

    public Transportation Transportation { get; } = null!;

    private RouteTransportation(double cost, int routeId, int transportationId)
    {
        if (cost <= 0)
        {
            throw new RouteTransportationInvalidCostException();
        }

        Cost = cost;
        RouteId = routeId;
        TransportationId = transportationId;
    }

    public static RouteTransportation Create(double cost, int routeId, int transportationId)
    {
        return new RouteTransportation(cost, routeId, transportationId);
    }
}