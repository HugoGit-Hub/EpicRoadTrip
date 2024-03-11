using EpicRoadTrip.Application.Roadtrips.GetRoadtrip;
using EpicRoadTrip.Application.Routes.GetRoute;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Routes;
using EpicRoadTrip.Domain.Transportations;
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
            var result = new List<GetRouteResponse>();
            foreach (var transportId in query.request.TransportationAllowedId)
            {
                switch(transportId)
                {
                    case (int)TransportationType.Train:
                        result.Add(routeService.GetRouteBetweenPointsByTrain(query.request.CityOneCoord, query.request.CityTwoCoord, cancellationToken).Adapt<GetRouteResponse>());
                        break;

                    default: 
                        break;
                }
            }
            return Result<IEnumerable<GetRouteResponse>>.Success(result);
        }
        else
        {
            return Result<IEnumerable<GetRouteResponse>>.Failure(new Error("999", "Undefined message error"));
        }
    }
}
