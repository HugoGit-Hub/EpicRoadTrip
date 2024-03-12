using EpicRoadTrip.Application.Routes.GetRoute;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Routes;
using EpicRoadTrip.Domain.Transportations;
using Mapster;
using System.Threading;
using System.Threading.Tasks;

namespace EpicRoadTrip.Application.Routes;

public class RouteService(
    IRouteRepository routeRepository )
    : IRouteService
{
    public Task<Result<IEnumerable<Route>>> GetRouteBetweenPoints(Tuple<float, float> cityOneCoord, Tuple<float, float> cityTwoCoord, IEnumerable<int> transportationAllowedIds, CancellationToken cancellationToken)
    {
        var result = new List<GetRouteResponse>();
        foreach (var transportId in transportationAllowedIds)
        {
            switch (transportId)
            {
                case (int)TransportationType.Train:
                    result.Add(routeRepository.FindTrainRoute(cityOneCoord, cityTwoCoord, cancellationToken).Adapt<GetRouteResponse>());
                    break;

                default:
                    throw new Exception("Transportation type not recognized");
            }
        }
        throw new Exception("Not fully implemented");
    }
}
