using EpicRoadTrip.Domain.CurrentUsers;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Roadtrips;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Roadtrips.GetRoadtrip;

public class GetRoadtripQueryHandler (
    ICurrentUserService currentUserService,
    IRoadtripService roadtripService)
    : IRequestHandler<GetRoadtripQuery, Result<GetRoadtripResponse>>
{
    public async Task<Result<GetRoadtripResponse>> Handle(GetRoadtripQuery query, CancellationToken cancellationToken)
    {
        var user = await currentUserService.GetCurrentUser(cancellationToken);
        if (user.IsFailure)
        {
            return Result<GetRoadtripResponse>.Failure(user.Error);
        }

        var roadtrip = await roadtripService.GetUserRoadtrip(query.Id, user.Value, cancellationToken);
        var getRoadtripResponse = roadtrip.Value.Adapt<GetRoadtripResponse>();
        
        return roadtrip.IsFailure 
            ? Result<GetRoadtripResponse>.Failure(RoadtripErrors.RoadtripNotFoundError) 
            : Result<GetRoadtripResponse>.Success(getRoadtripResponse);
    }
}