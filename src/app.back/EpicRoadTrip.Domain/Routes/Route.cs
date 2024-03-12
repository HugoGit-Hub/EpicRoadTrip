using EpicRoadTrip.Domain.Cities;
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

    public int CityOneId { get; }

    public int CityTwoId { get; }

    public int RoadtripId { get; }

    public string GeoJson { get; }

    public City CityOne { get; } = null!;

    public City CityTwo { get; } = null!;

    public Roadtrip Roadtrip { get; } = null!;

    public ICollection<Transportation> Transportations { get; } = [];
    
    private Route(
        int id,
        double distance,
        TimeSpan duration,
        int cityOneId,
        int cityTwoId,
        int roadtripId,
        string geoJson)
    {
        if (distance < 0)
        {
            throw new RouteInvalidDistanceException();
        }

        if (duration < TimeSpan.Zero)
        {
            throw new RouteInvalidDurationException();
        }

        if (cityOneId == cityTwoId)
        {
            throw new RouteInvalidCitiesException();
        }

        if (string.IsNullOrWhiteSpace(geoJson))
        {
            throw new RouteInvalidGeoJsonException();
        }

        Id = id;
        Distance = distance;
        Duration = duration;
        CityOneId = cityOneId;
        CityTwoId = cityTwoId;
        RoadtripId = roadtripId;
        GeoJson = geoJson;
    }

    public static Result<Route> Create(
        int id,
        double distance,
        TimeSpan duration,
        int cityOneId,
        int cityTwoId,
        int roadtripId,
        string geoJson)
    {
        try
        {
            var route = new Route(id, distance, duration, cityOneId, cityTwoId, roadtripId, geoJson);

            return Result<Route>.Success(route);
        }
        catch (Exception e)
        {
            return Result<Route>.Failure(GenericErrors<Route>.InvalidFormatError(e));
        }
    }
}