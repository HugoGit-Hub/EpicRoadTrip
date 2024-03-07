using EpicRoadTrip.Domain.Authentications;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Users;
using MediatR;
using System.Text.Json;

namespace EpicRoadTrip.Application.Authentication.Register;

public class RegisterCommandHandler(IAuthenticationService authenticationService, IUserService userService)
    : IRequestHandler<RegisterCommand, Result<string>>
{
    public async Task<Result<string>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var isEmailAlreadyUse = await authenticationService.IsEmailAlreadyUse(request.Email, cancellationToken);
        if (isEmailAlreadyUse)
        {
            return Result<string>.Failure(RegisterErrors.EmailAlreadyInUseError(request.Email));
        }

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

        var user = User.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            hashedAndSaltedPassword.Value,
            request.Age,
            request.Gender);
        if (user.IsFailure)
        {
            return Result<string>.Failure(user.Error);
        }

        var createUser = await userService.Create(user.Value, cancellationToken);
        if (createUser.IsFailure)
        {
            return Result<string>.Failure(createUser.Error);
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