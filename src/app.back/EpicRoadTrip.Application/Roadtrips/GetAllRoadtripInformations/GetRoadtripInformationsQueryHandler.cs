using EpicRoadTrip.Domain.ErrorHandling;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Roadtrips.GetAllRoadtripInformations;

public class GetRoadtripInformationsQueryHandler(IRoadtripRepository roadtripRepository)
    : IRequestHandler<GetRoadtripInformationsQuery, Result<GetRoadtripInformationsResponse>>
{
    public async Task<Result<GetRoadtripInformationsResponse>> Handle(GetRoadtripInformationsQuery query, CancellationToken cancellationToken)
    {
        var roadtrip = await roadtripRepository.GetByIdIncludeInstitutionsAndRoutes(query.Id, cancellationToken);
        if (roadtrip.IsFailure)
        {
            return Result<GetRoadtripInformationsResponse>.Failure(roadtrip.Error);
        }

        var response = roadtrip.Value.Adapt<GetRoadtripInformationsResponse>();

        return Result<GetRoadtripInformationsResponse>.Success(response);
    }
}