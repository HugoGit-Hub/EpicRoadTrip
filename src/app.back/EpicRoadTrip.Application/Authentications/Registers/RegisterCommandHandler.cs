using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.Authentications;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Users;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Authentications.Registers;

public class RegisterCommandHandler(
    IAuthenticationService authenticationService,
    IRepository<User> repository)
    : IRequestHandler<RegisterCommand, Result<RegisterResponse>>
{
    public async Task<Result<RegisterResponse>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var isEmailAlreadyUse = await authenticationService.IsEmailAlreadyUse(request.Email, cancellationToken);
        if (isEmailAlreadyUse)
        {
            return Result<RegisterResponse>.Failure(RegisterErrors.EmailAlreadyInUseError(request.Email));
        }

        var encryptedPassword = authenticationService.Encrypt(request.Password);
        if (encryptedPassword.IsFailure)
        {
            return Result<RegisterResponse>.Failure(encryptedPassword.Error);
        }

        var hashedAndSaltedPassword = authenticationService.HashWithSalt(encryptedPassword.Value);
        if (hashedAndSaltedPassword.IsFailure)
        {
            return Result<RegisterResponse>.Failure(hashedAndSaltedPassword.Error);
        }

        var user = User.Create(
            0,
            request.FirstName,
            request.LastName,
            request.Email,
            hashedAndSaltedPassword.Value,
            request.Age,
            request.Gender);
        if (user.IsFailure)
        {
            return Result<RegisterResponse>.Failure(user.Error);
        }

        var createUser = await repository.Create(user.Value, cancellationToken);
        if (createUser.IsFailure)
        {
            return Result<RegisterResponse>.Failure(createUser.Error);
        }

        var token = authenticationService.GenerateToken(user.Value);
        if (token.IsFailure)
        {
            return Result<RegisterResponse>.Failure(token.Error);
        }

        var response = createUser.Value.Adapt<RegisterResponse>();
        response = response with { Token = token.Value };

        return Result<RegisterResponse>.Success(response);
    }
}