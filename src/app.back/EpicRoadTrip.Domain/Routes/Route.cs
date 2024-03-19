using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.ErrorHandling.Generics;
using EpicRoadTrip.Domain.Roadtrips;
using EpicRoadTrip.Domain.Routes.Exceptions;
using EpicRoadTrip.Domain.Transportations;

namespace EpicRoadTrip.Domain.Routes;

public sealed class Route
{
    public int Id { get; }

    public double Distance { get; }

    public TimeSpan Duration { get; }

    public string CityOneName { get; }

    public string CityTwoName { get; }

    public Guid? RouteGroup { get; }

    public int RoadtripId { get; }

    public GeoJson GeoJson { get; }

    public Roadtrip Roadtrip { get; } = null!;

    public TransportationType TransportType { get; }

    #pragma warning disable CS8618
    public Route()
    {
    }

    private Route(
        int id,
        double distance,
        TimeSpan duration,
        string cityOneName,
        string cityTwoName,
        int roadtripId,
        GeoJson geoJson,
        Guid? routeGroup,
        TransportationType transportType
        )
    {
        if (distance < 0)
        {
            throw new RouteInvalidDistanceException();
        }

        if (duration < TimeSpan.Zero)
        {
            throw new RouteInvalidDurationException();
        }

        if (cityOneName == cityTwoName)
        {
            throw new RouteInvalidCitiesException();
        }

        Id = id;
        Distance = distance;
        Duration = duration;
        CityOneName = cityOneName;
        CityTwoName = cityTwoName;
        RoadtripId = roadtripId;
        GeoJson = geoJson;
        RouteGroup = routeGroup;
        TransportType = transportType;
    }

    public static Result<Route> Create(
        int id,
        double distance,
        TimeSpan duration,
        string cityOneName,
        string cityTwoName,
        int roadtripId,
        GeoJson geoJson,
        Guid? routeGroup,
        TransportationType transportType)
    {
        try
        {
            var route = new Route(id, distance, duration, cityOneName, cityTwoName, roadtripId, geoJson, routeGroup, transportType);
            return Result<Route>.Success(route);
        }
        catch (Exception e)
        {
            return Result<Route>.Failure(GenericErrors<Route>.InvalidFormatError(e));
        }
    }
}