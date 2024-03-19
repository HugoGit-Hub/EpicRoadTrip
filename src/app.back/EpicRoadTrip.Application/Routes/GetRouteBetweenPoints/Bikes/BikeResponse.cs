namespace EpicRoadTrip.Application.Routes.GetRouteBetweenPoints.Bikes;

public record BikeResponse
{
    public required List<Result> Results { get; init; }
}

public record Result
{
    public required string SearchId { get; init; }

    public required List<Location> Locations { get; init; }

    public required List<object> Unreachable { get; init; }
}

public record Location
{
    public required string Id { get; init; }

    public required List<Property> Properties { get; init; }
}

public record Property
{
    public int TravelTime { get; init; }

    public required Route Route { get; init; }
}

public record Route
{
    public DateTime DepartureTime { get; init; }

    public DateTime ArrivalTime { get; init; }

    public required List<Part> Parts { get; init; }
}

public record Part
{
    public int Id { get; init; }

    public required string Type { get; init; }

    public required string Mode { get; init; }

    public required string Directions { get; init; }

    public int Distance { get; init; }

    public int TravelTime { get; init; }

    public required List<Coord> Coords { get; init; }

    public required string Direction { get; init; }

    public required string Road { get; init; }

    public required string Turn { get; init; }
}

public record Coord
{
    public double Lat { get; init; }

    public double Lng { get; init; }
}
