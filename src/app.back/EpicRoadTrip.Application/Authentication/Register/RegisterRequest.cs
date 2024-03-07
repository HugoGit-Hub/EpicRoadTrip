namespace EpicRoadTrip.Application.Authentication.Register;

public record RegisterRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public int Age { get; init; }
    public bool Gender { get; init; }
}