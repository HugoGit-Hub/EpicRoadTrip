using EpicRoadTrip.Infrastructure.Externals.Train;
using EpicRoadTrip.Infrastructure.Routes;

namespace EpicRoadTrip.Test.Application.Routes;

[TestClass]
public class GetRouteBetweenPointsQueryHandlerTest
{
    [TestMethod]
    public async Task TestApiCall()
    {
        var coord1 = new Tuple<double, double>(3.88646f, 43.60824f);
        var coord2 = new Tuple<double, double>(7.25807f, 43.69961f);
        var ct = new CancellationToken();
        TrainClientGet trainC = new TrainClientGet(new HttpClient());
        var test = await new ExternalRouteService(trainC).FindTrainRoute(coord1, coord2, ct);
        Assert.IsTrue(test.IsSuccess );
    }
}
