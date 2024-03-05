using EpicRoadTrip.Domain.Roadtrips;
using EpicRoadTrip.Domain.Users.Exceptions;

namespace EpicRoadTrip.Domain.Users;

public sealed class User
{
    public int Id { get; }

    public string FirstName { get; }

    public string LastName { get; }

    public string Email { get; }

    public string Password { get; }

    public ICollection<Roadtrip> Roadtrips { get; } = [];
    
    private User(
        int id,
        string firstName,
        string lastName,
        string email,
        string password)
    {
        if (string.IsNullOrWhiteSpace(firstName)
            || string.IsNullOrWhiteSpace(lastName)
            || string.IsNullOrWhiteSpace(email)
            || string.IsNullOrWhiteSpace(password))
        {
            throw new UserInvalidFormatException();
        }

        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public static User Create(
        int id,
        string firstName,
        string lastName,
        string email,
        string password)
    {
        return new User(id, firstName, lastName, email, password);
    }
}