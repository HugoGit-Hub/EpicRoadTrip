namespace EpicRoadTrip.Application.Cities.CreateCity;

public record CreateCityResponse
{
    public int Id { get; init; }

    public required string Name { get; init;}
}