namespace EpicRoadTrip.Infrastructure.Externals.Bike;

public record BikeRequest(List<Location> Locations, List<DepartureSearch> DepartureSearches)
{
    public List<Location> Locations { get; set; } = Locations;

    public List<DepartureSearch> DepartureSearches { get; set; } = DepartureSearches;
}

public record Location(string Id, Coords Coords)
{
    public string Id { get; set; } = Id;

    public Coords Coords { get; set; } = Coords;
};

public record Coords(double Lat, double Lng);

public record DepartureSearch(
    Transportation Transportation, 
    DateTime DepartureTime,
    Range Range,
    List<string> ArrivalLocationIds,
    List<string> Properties,
    string Id = "departure-search",
    string DepartureLocationId = "point-from")
{
    public string Id { get; set; } = Id;

    public Transportation Transportation { get; set; } = Transportation;

    public string DepartureLocationId { get; set; } = DepartureLocationId;


    public List<string> ArrivalLocationIds { get; set; } = ArrivalLocationIds;

    public DateTime DepartureTime { get; set; } = DepartureTime;

    public List<string> Properties { get; set; } = Properties;

    public Range Range { get; init; } = Range;
}

public record Transportation(string Type = "cycling")
{
    public string Type { get; set; } = Type;
}

public record Range(bool Enabled = true, int MaxResults = 5, int Width = 900)
{
    public bool Enabled { get; set; } = Enabled;

    public int MaxResults { get; set; } = MaxResults;

    public int Width { get; set; } = Width;
}
