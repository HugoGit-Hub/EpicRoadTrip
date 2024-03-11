using EpicRoadTrip.Application.Routes;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Routes;
using EpicRoadTrip.Infrastructure.Context;
using EpicRoadTrip.Infrastructure.Externals;

namespace EpicRoadTrip.Infrastructure.Routes;

public class RouteRepository(EpicRoadTripContext context) : IRouteRepository
{
    Task<Result<Route>> IRouteRepository.FindTrainRoute(Tuple<float, float> cityOneCoord, Tuple<float, float> cityTwoCoord, CancellationToken cancellationToken)
    {
        ExternalClients.TrainClient.GetAsync("");
        throw new NotImplementedException();
    }
}
