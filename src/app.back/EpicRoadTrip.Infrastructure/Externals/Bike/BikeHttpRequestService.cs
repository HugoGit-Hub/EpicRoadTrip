using EpicRoadTrip.Application.Routes.GetRouteBetweenPoints.Bikes;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.HttpRequests;
using EpicRoadTrip.Domain.Routes;
using EpicRoadTrip.Domain.Transportations;
using EpicRoadTrip.Infrastructure.Externals.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Json;
using Route = EpicRoadTrip.Domain.Routes.Route;

namespace EpicRoadTrip.Infrastructure.Externals.Bike;

public class BikeHttpRequestService(HttpClient client) : IHttpRequestService<IEnumerable<Route>, BikeParameters>
{
    private const string PointFrom = "point-from";
    private const string PointToOne = "point-to-1";

    public async Task<Result<IEnumerable<Route>>> PostData(BikeParameters parameters, CancellationToken cancellationToken)
    {
        const string cityOneName = "foo";
        const string cityTwoName = "bar";
        const int roadTripId = 0;
        const string geoJsonType = "LineString";
        
        var locations = Locations(parameters);
        var departueSearches = DepartureSearchs();
        var request = new BikeRequest(locations, departueSearches.ToList());
        var settings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            },
            Formatting = Formatting.Indented
        };
        var json = JsonConvert.SerializeObject(request, settings);
        var response = await client.PostAsJsonAsync(DataSources.BikeParams, Json, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return Result<IEnumerable<Route>>.Failure(new Error("Error", "Bike service failed"));
        }

        var test = await response.Content.ReadAsStringAsync(cancellationToken);
        var externalBikeResponse = JsonConvert.DeserializeObject<BikeResponse>(test);
        if (externalBikeResponse is null)
        {
            return Result<IEnumerable<Route>>.Failure(new Error("Error", "Deserializatino failed"));
        }

        var parts = externalBikeResponse.Results[0].Locations[0].Properties[0].Route.Parts;
        var routes = new List<Route>();
        foreach (var part in parts)
        {
            var duration = TimeSpan.FromMinutes(part.TravelTime);
            var coordinates = part.Coords
                .Select(_ => Coordinate.Create(part.Coords[0].Lat, part.Coords[0].Lng))
                .Select(coordinate => coordinate.Value)
                .ToList();
            var geoJson = GeoJson.Create(geoJsonType, coordinates);
            if (geoJson.IsFailure)
            {
                return Result<IEnumerable<Route>>.Failure(new Error("Error", "GeoJson creation failed"));
            }

            var route = Route.Create(
                0,
                part.Distance,
                duration,
                cityOneName,
                cityTwoName,
                roadTripId,
                geoJson.Value,
                null,
                TransportationType.Bike);
            if (route.IsFailure)
            {
                return Result<IEnumerable<Route>>.Failure(new Error("Error", "Route creation failed"));
            }

            routes.Add(route.Value);
        }
        
        return Result<IEnumerable<Route>>.Success(routes);
    }

    public Task<Result<IEnumerable<Route>>> GetData(string url, BikeParameters query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private static IEnumerable<DepartureSearch> DepartureSearchs()
    {
        List<string> arrivalLocationIds = ["point-to-1"];
        List<string> properties = ["travel_time", "route"];
        var departueSearch = new DepartureSearch(
            new Transportation(),
            DateTime.Now,
            new Range(),
            arrivalLocationIds,
            properties);

        List<DepartureSearch> departureSearches = [departueSearch];
        return departureSearches;
    }

    private static List<Location> Locations(BikeParameters @params)
    {
        var startLocation = CreateLocation(PointFrom, @params.StartLocations.Lat, @params.StartLocations.Lng);
        var endLocation = CreateLocation(PointToOne, @params.EndLocations.Lat, @params.EndLocations.Lng);
        var locations = new List<Location> { startLocation, endLocation};

        return locations;
    }

    private static Location CreateLocation(string id, double lat, double lng) => new(id, new Coords(lat, lng));

    private static string Json =>
        "{\r\n  \"locations\": [\r\n    {\r\n      \"id\": \"point-from\",\r\n      \"coords\": {\r\n        \"lat\": 43.6112422,\r\n        \"lng\": 3.8767337\r\n      }\r\n    },\r\n    {\r\n      \"id\": \"point-to-1\",\r\n      \"coords\": {\r\n        \"lat\": 43.680784110954875,\r\n        \"lng\": 4.134980752290971\r\n      }\r\n    }\r\n  ],\r\n  \"departure_searches\": [\r\n    {\r\n      \"id\": \"departure-search\",\r\n      \"transportation\": {\r\n        \"type\": \"cycling\"\r\n      },\r\n      \"departure_location_id\": \"point-from\",\r\n      \"arrival_location_ids\": [\r\n        \"point-to-1\"\r\n      ],\r\n      \"departure_time\": \"2024-03-05T08:00:00.000Z\",\r\n      \"properties\": [\r\n        \"travel_time\",\r\n        \"route\"\r\n      ],\r\n      \"range\": {\r\n        \"enabled\": true,\r\n        \"max_results\": 5,\r\n        \"width\": 900\r\n      }\r\n    }\r\n  ]\r\n}";
}