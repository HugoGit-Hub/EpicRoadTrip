using EpicRoadTrip.Application.Routes;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Routes;
using EpicRoadTrip.Infrastructure.Externals.Train;

namespace EpicRoadTrip.Infrastructure.Routes;

public class ExternalRouteService(TrainClient trainClient) : IExternalRouteService
{
    public async Task<Result<Route>> FindTrainRoute(Tuple<float, float> cityOneCoord, Tuple<float, float> cityTwoCoord, CancellationToken cancellationToken)
    {
        var formattedParams = new Dictionary<string, string>
        {
            { "from", cityOneCoord.Item1.ToString().Replace(',', '.') + "%3B" + cityOneCoord.Item2.ToString().Replace(',', '.') },
            { "to", cityTwoCoord.Item1.ToString().Replace(',', '.') + "%3B" + cityTwoCoord.Item2.ToString().Replace(',', '.') }
        };

        Result<dynamic> test = await trainClient.GetData<dynamic>("journeys", formattedParams);
        dynamic o = test.Value;
        var t = o.journeys;
        var t1 = o.journeys;
        var t2 = o.journeys;
        throw new NotImplementedException();
    }
}
