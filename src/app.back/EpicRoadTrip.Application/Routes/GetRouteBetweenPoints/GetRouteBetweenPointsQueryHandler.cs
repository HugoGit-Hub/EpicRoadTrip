using EpicRoadTrip.Application.Routes.GetRoute;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Routes;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Routes.GetRouteBetweenPoints;

public class GetRouteBetweenPointsQueryHandler(IRouteService routeService)
    : IRequestHandler<GetRouteBetweenPointsQuery, Result<IEnumerable<GetRouteResponse>>>
{
    public async Task<Result<IEnumerable<GetRouteResponse>>> Handle(GetRouteBetweenPointsQuery query, CancellationToken cancellationToken)
    {
        if (query.Request.TransportationAllowedId.Count == 0
            || query.Request.CityOneCoord.Item1 == 0.0f
            || query.Request.CityOneCoord.Item2 == 0.0f
            || query.Request.CityTwoCoord.Item1 == 0.0f
            || query.Request.CityTwoCoord.Item2 == 0.0f)
        {
            return Result<IEnumerable<GetRouteResponse>>.Failure(new Error("999", "Undefined message error"));
        }
        
        var routes = await routeService.GetRouteBetweenPoints(query.Request.CityOneCoord, query.Request.CityTwoCoord, query.Request.TransportationAllowedId, cancellationToken);
        var result = routes.Value
            .Select(route => route.Adapt<GetRouteResponse>())
            .ToList();

        return Result<IEnumerable<GetRouteResponse>>.Success(result);
    }
}
