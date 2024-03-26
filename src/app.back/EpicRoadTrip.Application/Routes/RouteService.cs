using EpicRoadTrip.Application.Routes.GetRoute;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Routes;
using EpicRoadTrip.Domain.Transportations;
using Mapster;
using System.Collections.Generic;

namespace EpicRoadTrip.Application.Routes;

public class RouteService(IExternalRouteService extarExternalRouteService) : IRouteService
{
    public async Task<Result<IEnumerable<Route>>> GetRouteBetweenPoints(Tuple<float, float> cityOneCoord, Tuple<float, float> cityTwoCoord, IEnumerable<int> transportationAllowedIds, CancellationToken cancellationToken)
    {
        var result = new List<Route>();
        foreach (var transportId in transportationAllowedIds)
        {
            switch (transportId)
            {
                case (int)TransportationType.Train:
                    try
                    {
                        var trainRoutes = await extarExternalRouteService.FindTrainRoute(cityOneCoord, cityTwoCoord, cancellationToken);
                        if (trainRoutes.IsSuccess)
                        {
                            result.AddRange(trainRoutes.Value);
                        }
                        break;
                    }
                    catch
                    {
                        break;
                    }

                case (int)TransportationType.Car:
                    try
                    {
                        var CarRoutes = await extarExternalRouteService.FindCarRoute(cityOneCoord, cityTwoCoord, cancellationToken);
                        if (CarRoutes.IsSuccess)
                        {
                            result.AddRange(CarRoutes.Value);
                        }
                        break;
                    }
                    catch
                    {
                        break;
                    }

                case (int)TransportationType.Walk:
                    try
                    {
                        var walkRoutes = await extarExternalRouteService.FindPedestrianRoute(cityOneCoord, cityTwoCoord, cancellationToken);
                        if (walkRoutes.IsSuccess)
                        {
                            result.AddRange(walkRoutes.Value);
                        }
                        break;
                    }
                    catch
                    {
                        break;
                    }

                default:
                    throw new Exception("Transportation type not recognized");
            }
        }
        return Result<IEnumerable<Route>>.Success(result);
    }
}
