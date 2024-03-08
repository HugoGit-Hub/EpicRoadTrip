using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.ErrorHandling.Generics;
using EpicRoadTrip.Domain.Roadtrips;
using EpicRoadTrip.Domain.Users.Exceptions;

namespace EpicRoadTrip.Domain.Users;

public sealed class User
{
    public int Id { get; init; }

    public string FirstName { get; }

    public string LastName { get; }

    public string Email { get; }

    public string Password { get; }

    public int Age { get; }

    public bool Gender { get; }

    public ICollection<Roadtrip> Roadtrips { get; } = [];
    
    private User(
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

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        Age = age;
        Gender = gender;
    }

    public static Result<User> Create(
        string firstName,
        string lastName,
        string email,
        string password,
        int age,
        bool gender)
    {
        try
        {
            var user = new User(firstName, lastName, email, password, age, gender);

            return Result<User>.Success(user);
        }
        catch (Exception e)
        {
            return Result<User>.Failure(GenericErrors<User>.InvalidFormatError(e));
        }
    }
}