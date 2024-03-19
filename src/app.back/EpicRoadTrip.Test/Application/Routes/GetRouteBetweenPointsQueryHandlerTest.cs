using EpicRoadTrip.Application.Users;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Users;
using EpicRoadTrip.Infrastructure.Externals.Car;
using EpicRoadTrip.Infrastructure.Externals.Train;
using EpicRoadTrip.Infrastructure.Routes;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicRoadTrip.Test.Application.Routes;

[TestClass]
public class GetRouteBetweenPointsQueryHandlerTest
{
    //[TestMethod]
    //public async Task TestApiCall()
    //{
    //    var coord1 = new Tuple<float, float>(3.88646f, 43.60824f);
    //    var coord2 = new Tuple<float, float>(7.25807f, 43.69961f);
    //    var ct = new CancellationToken();
    //    TrainClient trainC = new TrainClient(new HttpClient());
    //    //TrainClient test = await new ExternalRouteService(trainC).FindTrainRoute(coord1, coord2, ct);
    //    Assert.IsTrue(test.IsSuccess );
    //}
}
