using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Routes;

namespace EpicRoadTrip.Application.Routes;

public interface IExternalRouteService
{
    public Task<Result<Route>> FindTrainRoute(Tuple<float, float> cityOneCoord, Tuple<float, float> cityTwoCoord, CancellationToken cancellationToken);
}
