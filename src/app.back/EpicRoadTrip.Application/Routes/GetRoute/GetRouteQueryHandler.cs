using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Routes;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Routes.GetRoute;

public class GetRouteQueryHandler(IRepository<Route> repository)
    : IRequestHandler<GetRouteQuery, Result<GetRouteResponse>>
{
    public async Task<Result<GetRouteResponse>> Handle(GetRouteQuery request, CancellationToken cancellationToken)
    {
        var route = await repository.GetById(request.Id, cancellationToken);
        var getRouteResponse = route.Value.Adapt<GetRouteResponse>();

        return route.IsFailure 
            ? Result<GetRouteResponse>.Failure(route.Error) 
            : Result<GetRouteResponse>.Success(getRouteResponse);
    }
}