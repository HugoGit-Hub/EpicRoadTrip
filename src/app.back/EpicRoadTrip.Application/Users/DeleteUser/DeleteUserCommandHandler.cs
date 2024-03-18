using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Users;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Users.DeleteUser;

public class DeleteUserCommandHandler(IRepository<User> repository)
    : IRequestHandler<DeleteUserCommand, Result<DeleteUserResponse>>
{
    public async Task<Result<DeleteUserResponse>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await repository.GetById(command.Id, cancellationToken);
        if (user.IsFailure)
        {
            return Result<DeleteUserResponse>.Failure(user.Error);
        }

        var delete = await repository.Delete(user.Value, cancellationToken);
        if (delete.IsFailure)
        {
            return Result<DeleteUserResponse>.Failure(delete.Error);
        }

        var response = delete.Value.Adapt<DeleteUserResponse>();

        return Result<DeleteUserResponse>.Success(response);
    }
}