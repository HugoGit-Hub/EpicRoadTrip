using EpicRoadTrip.Domain.ErrorHandling;

namespace EpicRoadTrip.Domain.Users;

public static class UserErrors
{
    public static Error UserNotFoundByEmailError => 
        new($"{nameof(User)}.{nameof(UserNotFoundByEmailError)}", "The user was not found by the provided email");

    public static Error UserInvalidFormatException(Exception e) => 
        new($"{nameof(User)}.{nameof(UserInvalidFormatException)}", $"The provided user is not in a valid format, {e}");

    public static Error ContextFailedToCreateUser(Exception e) =>
        new($"{nameof(User)}.{nameof(ContextFailedToCreateUser)}", $"Something went wrong at user context creation, {e}");
}