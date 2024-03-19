namespace EpicRoadTrip.Application.Routes.GetRouteBetweenPoints.Bikes;

public class BikeParameters
{
    public required Loc StartLocations { get; set; }

    public required Loc EndLocations { get; set; }
}

public record Loc(double Lat, double Lng);