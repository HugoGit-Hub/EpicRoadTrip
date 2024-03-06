using EpicRoadTrip.Domain.Cities;
using EpicRoadTrip.Domain.Roadtrips;
using EpicRoadTrip.Domain.Routes.Exceptions;

namespace EpicRoadTrip.Domain.Routes;

public sealed class Route
{
    public int Id { get; init; }

    public double Distance { get; }

    public TimeSpan Duration { get; }

    public int CityOneId { get; }

    public int CityTwoId { get; }

    public int RoadtripId { get; }

    public City CityOne { get; } = null!;

    public City CityTwo { get; } = null!;

    public Roadtrip Roadtrip { get; } = null!;
    
    private Route(
        double distance,
        TimeSpan duration,
        int cityOneId,
        int cityTwoId,
        int roadtripId)
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

        Distance = distance;
        Duration = duration;
        CityOneId = cityOneId;
        CityTwoId = cityTwoId;
        RoadtripId = roadtripId;
    }

    public static Route Create(
        double distance,
        TimeSpan duration,
        int cityOneId,
        int cityTwoId,
        int roadtripId)
    {
        return new Route(distance, duration, cityOneId, cityTwoId, roadtripId);
    }
}