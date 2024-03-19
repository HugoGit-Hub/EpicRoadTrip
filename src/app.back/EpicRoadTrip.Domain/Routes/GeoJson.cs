using EpicRoadTrip.Domain.ErrorHandling;

namespace EpicRoadTrip.Domain.Routes;

public class GeoJson
{
    public string Type { get; }
    
    public List<Coordinate> Coordinates { get; }

    #pragma warning disable CS8618
    private GeoJson() { }
    
    private GeoJson(
        string type,
        List<Coordinate> coordinates)
    {
        Type = type;
        Coordinates = coordinates;
    }

    public static Result<GeoJson> Create(
        string type,
        List<Coordinate> coordinates)
    {
        try
        {
            return Result<GeoJson>.Success(new GeoJson(type, coordinates));
        }
        catch (Exception e)
        {
            return Result<GeoJson>.Failure(new Error("Error", e.Message));
        }
    }
}