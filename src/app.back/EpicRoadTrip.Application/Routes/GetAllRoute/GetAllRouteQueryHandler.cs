using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Routes;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Routes.GetAllRoute;

public class GetAllRouteQueryHandler(IRepository<Route> repository)
    : IRequestHandler<GetAllRouteQuery, Result<IEnumerable<GetAllRouteResponse>>>
{
    public Task<Result<IEnumerable<GetAllRouteResponse>>> Handle(GetAllRouteQuery query, CancellationToken cancellationToken)
    {
        var getAll = repository.GetAll();
        var getAllRouteResponse = getAll.Value.Adapt<IEnumerable<GetAllRouteResponse>>();
        
        return Task.FromResult(getAll.IsFailure
            ? Result<IEnumerable<GetAllRouteResponse>>.Failure(getAll.Error)
            : Result<IEnumerable<GetAllRouteResponse>>.Success(getAllRouteResponse));
    }
}