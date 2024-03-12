using EpicRoadTrip.Application.Routes.GetRoute;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Routes;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Routes.GetRouteBetweenPoints;

public class GetRouteBetweenPointsQueryHandler(
    IRouteService routeService)
    : IRequestHandler<GetRouteBetweenPointsQuery, Result<IEnumerable<GetRouteResponse>>>
{
    public async Task<Result<IEnumerable<GetRouteResponse>>> Handle(GetRouteBetweenPointsQuery query, CancellationToken cancellationToken)
    {
        if ( query.request.TransportationAllowedId.Any()
            && query.request.CityOneCoord.Item1 != default
            && query.request.CityOneCoord.Item2 != default
            && query.request.CityTwoCoord.Item1 != default 
            && query.request.CityTwoCoord.Item2 != default)
        {
            var result = new List<GetRouteResponse>
            {
                routeService.GetRouteBetweenPoints(query.request.CityOneCoord, query.request.CityTwoCoord, query.request.TransportationAllowedId, cancellationToken).Adapt<GetRouteResponse>()
            };
            return Result<IEnumerable<GetRouteResponse>>.Success(result);
        }
        else
        {
            return Result<IEnumerable<GetRouteResponse>>.Failure(new Error("999", "Undefined message error"));
        }
    }
}
