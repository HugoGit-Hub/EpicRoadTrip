namespace EpicRoadTrip.Application.Cities.UpdateCity;

public record UpdateCityResponse
{
    public int Id { get; init; }

    public required string Name { get; init; }
}