using EpicRoadTrip.Application.Routes.GetRouteBetweenPoints.Bikes;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.External;
using EpicRoadTrip.Domain.HttpRequests;
using EpicRoadTrip.Domain.Routes;
using EpicRoadTrip.Domain.Transportations;
using Route = EpicRoadTrip.Domain.Routes.Route;

namespace EpicRoadTrip.Application.Routes;

public class RouteService(
    IExternalRouteService externalRouteService,
    IHttpRequestService<IEnumerable<Route>, BikeParameters> httpRequestService) 
    : IRouteService
{
    public async Task<Result<IEnumerable<Route>>> GetRouteBetweenPoints(
        Tuple<double, double> cityOneCoord,
        Tuple<double, double> cityTwoCoord,
        IEnumerable<TransportationType> transportationAllowedIds, CancellationToken cancellationToken)
    {
        var routes = new List<Route>();
        foreach (var transportId in transportationAllowedIds)
        {
            switch (transportId)
            {
                case TransportationType.Train:
                    routes.AddRange(externalRouteService.FindTrainRoute(cityOneCoord, cityTwoCoord, cancellationToken).Result.Value);
                    break;
                case TransportationType.Walk:
                case TransportationType.Bike:
                    var result = await GetBikeRoutes(cityOneCoord, cityTwoCoord, cancellationToken);
                    if (result.IsFailure)
                    {
                        return Result<IEnumerable<Route>>.Failure(new Error("Error", "Error"));
                    }

                    routes.AddRange(result.Value);
                    break;
                case TransportationType.Airplane:
                case TransportationType.Bus:
                case TransportationType.Car:
                default:
                    throw new Exception("Transportation type not recognized");
            }
        }

        return Result<IEnumerable<Route>>.Success(routes);
    }

    private async Task<Result<IEnumerable<Route>>> GetBikeRoutes(
        Tuple<double, double> cityOneCoord,
        Tuple<double, double> cityTwoCoord,
        CancellationToken cancellationToken)
    {
        var bikeParameters = new BikeParameters
        {
            StartLocations = new Loc(cityOneCoord.Item1, cityOneCoord.Item2),
            EndLocations = new Loc(cityTwoCoord.Item1, cityTwoCoord.Item2)
        };

        var response = await httpRequestService.PostData(bikeParameters, cancellationToken);
        
        return response.IsFailure 
            ? Result<IEnumerable<Route>>.Failure(new Error("Error", "Error")) 
            : Result<IEnumerable<Route>>.Success(response.Value);
    }
}
