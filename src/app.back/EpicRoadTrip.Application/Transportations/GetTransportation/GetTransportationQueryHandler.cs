using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Transportations;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Transportations.GetTransportation;

public class GetTransportationQueryHandler(IRepository<Transportation> repository)
    : IRequestHandler<GetTransportationQuery, Result<GetTransportationResponse>>
{
    public async Task<Result<GetTransportationResponse>> Handle(GetTransportationQuery query, CancellationToken cancellationToken)
    {
        var get = await repository.GetById(query.Id, cancellationToken);
        var getTransportationResponse = get.Value.Adapt<GetTransportationResponse>();
        
        return get.IsFailure 
            ? Result<GetTransportationResponse>.Failure(get.Error) 
            : Result<GetTransportationResponse>.Success(getTransportationResponse);
    }
}