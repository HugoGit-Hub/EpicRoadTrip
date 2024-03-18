using EpicRoadTrip.Application.Users;
using EpicRoadTrip.Domain.Authentications;
using EpicRoadTrip.Domain.ErrorHandling;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Authentications.Logins;

public class LoginCommandHandler(
    IAuthenticationService authenticationService,
    IUserRepository userRepository) 
    : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    public async Task<Result<LoginResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var encryptedPassword = authenticationService.Encrypt(request.Password);
        if (encryptedPassword.IsFailure)
        {
            return Result<LoginResponse>.Failure(encryptedPassword.Error);
        }

        var hashedAndSaltedPassword = authenticationService.HashWithSalt(encryptedPassword.Value);
        if (hashedAndSaltedPassword.IsFailure)
        {
            return Result<LoginResponse>.Failure(hashedAndSaltedPassword.Error);
        }

        var areCredentialscorrects = await authenticationService.AreCredentialscorrects(request.Email, hashedAndSaltedPassword.Value, cancellationToken);
        if (!areCredentialscorrects)
        {
            return Result<LoginResponse>.Failure(AuthenticationErrors.InvalidEmailOrPasswordError);
        }

        var user = await userRepository.GetByEmailIncludRoadtrips(request.Email, cancellationToken);
        if (user.IsFailure)
        {
            return Result<LoginResponse>.Failure(user.Error);
        }

        var token = authenticationService.GenerateToken(user.Value);
        if (token.IsFailure)
        {
            return Result<LoginResponse>.Failure(token.Error);
        }

        var response = user.Value.Adapt<LoginResponse>();
        response = response with { Token = token.Value };

        return Result<LoginResponse>.Success(response);
    }
}