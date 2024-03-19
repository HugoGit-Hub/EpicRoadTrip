using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Routes;

namespace EpicRoadTrip.Domain.External;

public interface IExternalRouteService
{
    public Task<Result<IEnumerable<Route>>> FindTrainRoute(
        Tuple<double, double> cityOneCoord,
        Tuple<double, double> cityTwoCoord, 
        CancellationToken cancellationToken);
}
