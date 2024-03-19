using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.CurrentUsers;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Roadtrips;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Roadtrips.CreateRoadtrip;

public class CreateRoadtripCommandHandler(
    ICurrentUserService currentUserService,
    IRepository<Roadtrip> repository) 
    : IRequestHandler<CreateRoadtripCommand, Result<CreateRoadtripResponse>>
{
    public async Task<Result<CreateRoadtripResponse>> Handle(CreateRoadtripCommand command, CancellationToken cancellationToken)
    {
        var user = await currentUserService.GetCurrentUser(cancellationToken);
        if (user.IsFailure)
        {
            return Result<CreateRoadtripResponse>.Failure(user.Error);
        }

        var request = command.Request;
        var roadtrip = Roadtrip.Create(
            0,
            request.Budget,
            request.StartDate,
            request.EndDate,
            user.Value.Id,
            request.Duration,
            request.NbTransfers,
            request.Tags,
            request.Co2Emission);
        if (roadtrip.IsFailure)
        {
            return Result<CreateRoadtripResponse>.Failure(roadtrip.Error);
        }

        var create = await repository.Create(roadtrip.Value, cancellationToken);
        var createRoadtripResponse = create.Value.Adapt<CreateRoadtripResponse>();

        return create.IsFailure 
            ? Result<CreateRoadtripResponse>.Failure(create.Error) 
            : Result<CreateRoadtripResponse>.Success(createRoadtripResponse);
    }
}