namespace EpicRoadTrip.Application.Authentications.Registers;

public record RegisterResponse
{
    public int Id { get; init; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string Email { get; init; }

    public int Age { get; init; }

    public bool Gender { get; init; }

    public required string Token { get; init; }
}