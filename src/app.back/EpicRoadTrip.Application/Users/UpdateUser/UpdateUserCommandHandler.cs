using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.Authentications;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Users;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Users.UpdateUser;

public class UpdateUserCommandHandler(
    IRepository<User> repository,
    IAuthenticationService authenticationService)
    : IRequestHandler<UpdateUserCommand, Result<UpdateUserResponse>>
{
    public async Task<Result<UpdateUserResponse>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var encryptPassword = authenticationService.Encrypt(request.Password);
        if (encryptPassword.IsFailure)
        {
            return Result<UpdateUserResponse>.Failure(encryptPassword.Error);
        }

        var hashWithSaltPassword = authenticationService.HashWithSalt(encryptPassword.Value);
        if (hashWithSaltPassword.IsFailure)
        {
            return Result<UpdateUserResponse>.Failure(hashWithSaltPassword.Error);
        }

        var user = User.Create(
            request.Id,
            request.FirstName,
            request.LastName,
            request.Email,
            hashWithSaltPassword.Value,
            request.Age,
            request.Gender);
        if (user.IsFailure)
        {
            return Result<UpdateUserResponse>.Failure(user.Error);
        }

        var result = await repository.Update(user.Value, cancellationToken);
        if (result.IsFailure)
        {
            return Result<UpdateUserResponse>.Failure(result.Error);
        }

        var response = user.Value.Adapt<UpdateUserResponse>();
        response = response with
        {
            Password = request.Password
        };

        return Result<UpdateUserResponse>.Success(response);
    }
}