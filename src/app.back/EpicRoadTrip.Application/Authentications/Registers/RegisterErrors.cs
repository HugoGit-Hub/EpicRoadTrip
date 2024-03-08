using EpicRoadTrip.Domain.ErrorHandling;

namespace EpicRoadTrip.Application.Authentications.Registers;

public static class RegisterErrors
{
    public static Error EmailAlreadyInUseError(string email) => new(
        $"Register.{nameof(EmailAlreadyInUseError)}", $"The provided email : {email} is already used");
}