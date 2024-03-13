namespace EpicRoadTrip.Application.Cities.GetCity;

public record GetCityResponse
{
    public int Id { get; init; }

    public required string Name { get; init; }
}