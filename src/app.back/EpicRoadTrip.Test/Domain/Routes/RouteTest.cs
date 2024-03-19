using EpicRoadTrip.Domain.Routes;
using EpicRoadTrip.Domain.Transportations;

namespace EpicRoadTrip.Test.Domain.Routes;

[TestClass]
public class RouteTest
{
    private const int Id = 1;
    private const double Distance = 100.5;
    private const string CityOneName = "Tralala";
    private const string CityTwoName = "Tralalalala";
    private const int RoadtripId = 1;
    private const string GeoJson = "geo";
    private const TransportationType Ttype = TransportationType.Train;
    private readonly Guid _guidGen = Guid.NewGuid();

    private static readonly TimeSpan Duration = TimeSpan.FromHours(1);

    [TestMethod]
    public void CreateRoute_WithValidData_ShouldCreateRoute()
    {
        // Act
        var route = Route.Create(Id, Distance, Duration, CityOneName, CityTwoName, RoadtripId, GeoJson, _guidGen, Ttype);

        // Assert
        Assert.AreEqual(Distance, route.Value.Distance);
        Assert.AreEqual(Duration, route.Value.Duration);
        Assert.AreEqual(CityOneName, route.Value.CityOneName);
        Assert.AreEqual(CityTwoName, route.Value.CityTwoName);
        Assert.AreEqual(RoadtripId, route.Value.RoadtripId);
    }

    [TestMethod]
    public void CreateRoute_WithNegativeDistance_ShouldThrowException()
    {
        // Arrange
        const double distance = -1;

        // Act
        var result = Route.Create(Id, distance, Duration, CityOneName, CityTwoName, RoadtripId, GeoJson, _guidGen, Ttype);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }

    [TestMethod]
    public void CreateRoute_WithNegativeDuration_ShouldThrowException()
    {
        // Arrange
        var duration = TimeSpan.FromHours(-1);

        // Act
        var result = Route.Create(Id, Distance, duration, CityOneName, CityTwoName, RoadtripId, GeoJson, _guidGen, Ttype);
    
        // Assert
        Assert.IsTrue(result.IsFailure);
    }

    [TestMethod]
    public void CreateRoute_WithSameCityIds_ShouldThrowException()
    {
        // Arrange
        const string CityTwoName = "Tralala";

        // Act
        var result = Route.Create(Id, Distance, Duration, CityOneName, CityTwoName, RoadtripId, GeoJson, _guidGen, Ttype);
    
        // Assert
        Assert.IsTrue(result.IsFailure);
    }
}