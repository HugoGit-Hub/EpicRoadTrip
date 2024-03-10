using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.CurrentUsers;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Roadtrips;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Roadtrips.UpdateRoadtrip;

public class UpdateRoadtripCommandHandler (
    ICurrentUserService currentUserService,
    IRepository<Roadtrip> repository)
    : IRequestHandler<UpdateRoadtripCommand, Result<UpdateRoadtripResponse>>
{
    public async Task<Result<UpdateRoadtripResponse>> Handle(UpdateRoadtripCommand command, CancellationToken cancellationToken)
    {
        var user = await currentUserService.GetCurrentUser(cancellationToken);
        if (user.IsFailure)
        {
            return Result<UpdateRoadtripResponse>.Failure(user.Error);
        }

        var request = command.Request;
        var roadtrip = Roadtrip.Create(request.Id, request.Budget, request.StartDate, request.EndDate, user.Value.Id);
        if (roadtrip.IsFailure)
        {
            return Result<UpdateRoadtripResponse>.Failure(roadtrip.Error);
        }

        var update = await repository.Update(roadtrip.Value, cancellationToken);
        var updateRoadtripResponse = update.Value.Adapt<UpdateRoadtripResponse>();

        return update.IsFailure 
            ? Result<UpdateRoadtripResponse>.Failure(update.Error) 
            : Result<UpdateRoadtripResponse>.Success(updateRoadtripResponse);
    }
}