using EpicRoadTrip.Domain.ErrorHandling;

namespace EpicRoadTrip.Application.Authentications;

public static class AuthenticationErrors
{
    private const string Authentication = "Authentication";

    public static Error EncryptionKeyNotFoundError =>
        new($"{Authentication}.{nameof(EncryptionKeyNotFoundError)}", "Encryption key not found in the configuration appsettings files.");

    public static Error SaltNotFoundError =>
        new($"{Authentication}.{nameof(SaltNotFoundError)}", "Salt not found in the configuration appsettings files.");
    
    public static Error TokenGenerationConfigurationError =>
        new($"{Authentication}.{nameof(TokenGenerationConfigurationError)}", "Token generation configuration error.");

    public static Error InvalidEmailOrPasswordError =>
        new($"{Authentication}.{nameof(InvalidEmailOrPasswordError)}", "Invalid email or password.");

    public static Error EncryptionFailedError(Exception e) =>
        new($"{Authentication}.{nameof(EncryptionFailedError)}", $"Failed to encrypt the content : {e}");

    public static Error HashingFailedError(Exception e) =>
        new($"{Authentication}.{nameof(HashingFailedError)}", $"Failed to hash the content : {e}");

    public static Error TokenGenerationFailedError(Exception e) =>
        new($"{Authentication}.{nameof(TokenGenerationFailedError)}", $"Failed to generate token : {e}");
}