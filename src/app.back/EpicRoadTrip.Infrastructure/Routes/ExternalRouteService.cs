using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.External;
using EpicRoadTrip.Domain.Routes;
using EpicRoadTrip.Domain.Transportations;

namespace EpicRoadTrip.Infrastructure.Routes;

public class ExternalRouteService(IExternalClientGet trainClientGet) : IExternalRouteService
{
    public async Task<Result<IEnumerable<Route>>> FindTrainRoute(Tuple<double, double> cityOneCoord,
        Tuple<double, double> cityTwoCoord, CancellationToken cancellationToken)
    {
        var formattedParams = new Dictionary<string, string>
        {
            { "from", cityOneCoord.Item1.ToString().Replace(',', '.') + "%3B" + cityOneCoord.Item2.ToString().Replace(',', '.') },
            { "to", cityTwoCoord.Item1.ToString().Replace(',', '.') + "%3B" + cityTwoCoord.Item2.ToString().Replace(',', '.') }
        };

        Result<dynamic> dResult = await trainClientGet.GetData<dynamic>("journeys", formattedParams);

        if (dResult.IsSuccess)
        {
            dynamic dJson = dResult.Value;
            var routesResult = new List<Route>();

            foreach (var currentJourney in dJson.journeys)
            {
                var routeGroup = Guid.NewGuid();

                foreach (var currentSection in currentJourney.sections)
                {
                    if(currentSection.geojson != null)
                    {
                        var geoJson = currentSection.geojson.ToString();
                        var duration = new TimeSpan(currentSection.duration.Value * 10000000);
                        var transportType = TransportationType.Train;
                        if (currentSection.mode != null)
                        {
                            transportType = TransportationType.Walk;
                        }
                        Result<Route> correspondingRoute = Route.Create(
                            0,
                            0,
                            duration,
                            currentSection.from.name.Value,
                            currentSection.to.name.Value,
                            -1,
                            geoJson,
                            routeGroup,
                            transportType
                            );

                        if (correspondingRoute.IsSuccess)
                        {
                            routesResult.Add(correspondingRoute.Value);
                        }
                    }
                }
            }

            return Result<IEnumerable<Route>>.Success(routesResult);
        }
        else
        {
            return Result<IEnumerable<Route>>.Failure(new Error("", ""));
        }
    }
}
