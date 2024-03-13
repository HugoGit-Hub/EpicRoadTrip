namespace EpicRoadTrip.Application.Cities.UpdateCity;

public record UpdateCityRequest
{
    public int Id { get; init; }

    public required string Name { get; init; }
}