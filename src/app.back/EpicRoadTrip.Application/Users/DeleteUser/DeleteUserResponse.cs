namespace EpicRoadTrip.Application.Users.DeleteUser;

public record DeleteUserResponse
{
    public int Id { get; init; }

    public required string Email { get; init; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public int Age { get; init; }

    public bool Gender { get; init; }
}