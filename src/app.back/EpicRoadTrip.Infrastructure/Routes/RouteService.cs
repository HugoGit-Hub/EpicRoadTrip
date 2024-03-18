using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Routes;

namespace EpicRoadTrip.Infrastructure.Routes;

public class RouteService : IRouteService
{
    public Task<Result<IEnumerable<Route>>> GetRouteBetweenPoints(Tuple<float, float> cityOneCoord, Tuple<float, float> cityTwoCoord, IEnumerable<int> transportationAllowedIds,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
