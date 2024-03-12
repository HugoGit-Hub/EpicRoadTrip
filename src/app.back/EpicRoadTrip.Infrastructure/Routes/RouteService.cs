using EpicRoadTrip.Application.Routes;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Routes;
using EpicRoadTrip.Infrastructure.Context;
using EpicRoadTrip.Infrastructure.Externals;

namespace EpicRoadTrip.Infrastructure.Routes;

public class RouteService() : IRouteService
{
    public async Task<Result<Route>> FindTrainRoute(Tuple<float, float> cityOneCoord, Tuple<float, float> cityTwoCoord, CancellationToken cancellationToken)
    {
        var test = await ExternalClients.TrainClient.GetAsync("/journeys?from=3.88646%3B43.60824&to=7.25807%3B43.69961");
        throw new NotImplementedException();
    }
}
