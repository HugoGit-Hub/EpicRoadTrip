using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.CurrentUsers;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Roadtrips;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Roadtrips.DeleteRoadtrip;

public class DeleteRoadtripCommandHandler(
    ICurrentUserService currentUserService,
    IRoadtripService roadtripService,
    IRepository<Roadtrip> repository)
    : IRequestHandler<DeleteRoadtripCommand, Result<DeleteRoadtripResponse>>
{
    public async Task<Result<DeleteRoadtripResponse>> Handle(DeleteRoadtripCommand command, CancellationToken cancellationToken)
    {
        var user = await currentUserService.GetCurrentUser(cancellationToken);
        if (user.IsFailure)
        {
            return Result<DeleteRoadtripResponse>.Failure(user.Error);
        }

        var roadtrip = await roadtripService.GetUserRoadtrip(command.Id, user.Value, cancellationToken);
        if (roadtrip.IsFailure)
        {
            return Result<DeleteRoadtripResponse>.Failure(roadtrip.Error);
        }

        var delete = await repository.Delete(roadtrip.Value, cancellationToken);
        var deleteRoadtripResponse = delete.Value.Adapt<DeleteRoadtripResponse>();

        return delete.IsFailure 
            ? Result<DeleteRoadtripResponse>.Failure(delete.Error) 
            : Result<DeleteRoadtripResponse>.Success(deleteRoadtripResponse);
    }
}