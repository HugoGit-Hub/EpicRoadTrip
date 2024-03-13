using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Application.Transportations.GetTransportation;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Transportations;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Transportations.GetAllTransportation;

public class GetAllTransportationQueryHandler(IRepository<Transportation> repository)
    : IRequestHandler<GetAllTransportationQuery, Result<IEnumerable<GetTransportationResponse>>>
{
    public Task<Result<IEnumerable<GetTransportationResponse>>> Handle(GetAllTransportationQuery request, CancellationToken cancellationToken)
    {
        var getAll = repository.GetAll();
        var getAllResponse = getAll.Value.Adapt<IEnumerable<GetTransportationResponse>>();
        
        return Task.FromResult(getAll.IsFailure 
            ? Result<IEnumerable<GetTransportationResponse>>.Failure(getAll.Error) 
            : Result<IEnumerable<GetTransportationResponse>>.Success(getAllResponse));
    }
}