using EpicRoadTrip.Application.Roadtrips.GetRoadtrip;
using EpicRoadTrip.Domain.CurrentUsers;
using EpicRoadTrip.Domain.ErrorHandling;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Roadtrips.GetAllRoadtrip;

public class GetAllRoadtripQueryHandler(ICurrentUserService currentUserService)
    : IRequestHandler<GetAllRoadtripQuery, Result<IEnumerable<GetRoadtripResponse>>>
{
    public async Task<Result<IEnumerable<GetRoadtripResponse>>> Handle(GetAllRoadtripQuery request, CancellationToken cancellationToken)
    {
        var user = await currentUserService.GetCurrentUser(cancellationToken);
        if (user.IsFailure)
        {
            return Result<IEnumerable<GetRoadtripResponse>>.Failure(user.Error);
        }

        var roadtrips = user.Value.Roadtrips;
        var getRoadtripResponses = roadtrips.Adapt<IEnumerable<GetRoadtripResponse>>();

        return Result<IEnumerable<GetRoadtripResponse>>.Success(getRoadtripResponses);
    }
}