using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Routes;

namespace EpicRoadTrip.Domain.Routes;

public interface IExternalRouteService
{
    public Task<Result<IEnumerable<Route>>> FindTrainRoute(Tuple<float, float> cityOneCoord, Tuple<float, float> cityTwoCoord, CancellationToken cancellationToken);
    public Task<Result<IEnumerable<Route>>> FindCarRoute(Tuple<float, float> cityOneCoord, Tuple<float, float> cityTwoCoord, CancellationToken cancellationToken);
}
