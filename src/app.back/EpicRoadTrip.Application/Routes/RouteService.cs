using EpicRoadTrip.Application.Routes.GetRoute;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Routes;
using EpicRoadTrip.Domain.Transportations;
using Mapster;

namespace EpicRoadTrip.Application.Routes;

public class RouteService(IExternalRouteService extarExternalRouteService) : IRouteService
{
    public Result<IEnumerable<Route>> GetRouteBetweenPoints(Tuple<float, float> cityOneCoord, Tuple<float, float> cityTwoCoord, IEnumerable<int> transportationAllowedIds, CancellationToken cancellationToken)
    {
        var result = new List<Route>();
        foreach (var transportId in transportationAllowedIds)
        {
            switch (transportId)
            {
                case (int)TransportationType.Train:
                    result.AddRange(extarExternalRouteService.FindTrainRoute(cityOneCoord, cityTwoCoord, cancellationToken).Result.Value);
                    break;

                case (int)TransportationType.Car:
                    result.AddRange(extarExternalRouteService.FindCarRoute(cityOneCoord, cityTwoCoord, cancellationToken).Result.Value);
                    break;

                default:
                    throw new Exception("Transportation type not recognized");
            }
        }
        return Result<IEnumerable<Route>>.Success(result);
    }
}
