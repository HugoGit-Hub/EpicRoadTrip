using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Routes.Exceptions;

namespace EpicRoadTrip.Domain.Routes;

public class Coordinate
{
    public double Latitude { get; }
    
    public double Longitude { get; }

    private Coordinate(double latitude, double longitude)
    {
        if (latitude is < -90 or > 90)
        {
            throw new CoordinateInvalidLatitudeException();
        }

        if (longitude is < -180 or > 180)
        {
            throw new CoordinateInvalidLongitudeException();
        }
        
        Latitude = latitude;
        Longitude = longitude;
    }

    public static Result<Coordinate> Create(double latitude, double longitude)
    {
        try
        {
            var coordinate = new Coordinate(latitude, longitude);
            return Result<Coordinate>.Success(coordinate);
        }
        catch (Exception e)
        {
            return Result<Coordinate>.Failure(new Error("Error", e.Message));
        }
    }
}