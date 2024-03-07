using EpicRoadTrip.Domain.ErrorHandling;

namespace EpicRoadTrip.Domain.Users;

public static class UserErrors
{
    public static Error UserInvalidFormatException(Exception e) => new(
               $"{nameof(User)}.{nameof(UserInvalidFormatException)}", $"The provided user is not in a valid format, {e}");
}