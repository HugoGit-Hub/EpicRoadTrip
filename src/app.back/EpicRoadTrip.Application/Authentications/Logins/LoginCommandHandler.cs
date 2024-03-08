using EpicRoadTrip.Domain.Authentications;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Users;
using MediatR;
using System.Text.Json;

namespace EpicRoadTrip.Application.Authentications.Logins;

public class LoginCommandHandler(
    IAuthenticationService authenticationService,
    IUserService userService) 
    : IRequestHandler<LoginCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var encryptedPassword = authenticationService.Encrypt(request.Password);
        if (encryptedPassword.IsFailure)
        {
            return Result<string>.Failure(encryptedPassword.Error);
        }

        var hashedAndSaltedPassword = authenticationService.HashWithSalt(encryptedPassword.Value);
        if (hashedAndSaltedPassword.IsFailure)
        {
            return Result<string>.Failure(hashedAndSaltedPassword.Error);
        }

        var areCredentialscorrects = await authenticationService.AreCredentialscorrects(request.Email, hashedAndSaltedPassword.Value, cancellationToken);
        if (!areCredentialscorrects)
        {
            return Result<string>.Failure(AuthenticationErrors.InvalidEmailOrPasswordError);
        }

        var user = await userService.GetByEmail(request.Email, cancellationToken);
        if (user.IsFailure)
        {
            return Result<string>.Failure(user.Error);
        }

        var token = authenticationService.GenerateToken(user.Value);
        if (token.IsFailure)
        {
            return Result<string>.Failure(token.Error);
        }

        var json = JsonSerializer.Serialize(new {Token = token.Value});

        return Result<string>.Success(json);
    }
}