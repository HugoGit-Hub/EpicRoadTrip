using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Transportations;

namespace EpicRoadTrip.Domain.Routes;

public interface IRouteService
{
    public Task<Result<IEnumerable<Route>>> GetRouteBetweenPoints(
        Tuple<double, double> cityOneCoord,
        Tuple<double, double> cityTwoCoord,
        IEnumerable<TransportationType> transportationAllowedIds,
        CancellationToken cancellationToken);
}