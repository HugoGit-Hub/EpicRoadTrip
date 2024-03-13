namespace EpicRoadTrip.Application.Cities.DeleteCity;

public record DeleteCityResponse
{
    public int Id { get; init; }

    public required string Name { get; init; }
}