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

    public int Age { get; }

    public bool Gender { get; }

    public ICollection<Roadtrip> Roadtrips { get; } = [];
    
    private User(
        int id,
        string firstName,
        string lastName,
        string email,
        string password,
        int age,
        bool gender)
    {
        if (string.IsNullOrWhiteSpace(firstName)
            || string.IsNullOrWhiteSpace(lastName)
            || string.IsNullOrWhiteSpace(email)
            || string.IsNullOrWhiteSpace(password))
        {
            throw new UserInvalidFormatException();
        }

        if (age < 0)
        {
            throw new UserInvalidAgeException();
        }

        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        Age = age;
        Gender = gender;
    }

    public static User Create(
        int id,
        string firstName,
        string lastName,
        string email,
        string password,
        int age,
        bool gender)
    {
        return new User(id, firstName, lastName, email, password, age, gender);
    }
}