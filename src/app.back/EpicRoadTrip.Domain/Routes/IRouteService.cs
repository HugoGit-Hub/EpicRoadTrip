using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Users;

namespace EpicRoadTrip.Domain.Routes;

public interface IRouteService
{
    public Task<Result<IEnumerable<Route>>> GetRouteBetweenPoints(Tuple<float, float> cityOneCoord, Tuple<float, float> cityTwoCoord, IEnumerable<int> transportationAllowedIds, CancellationToken cancellationToken);
}