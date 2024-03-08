using EpicRoadTrip.Domain.Routes;

namespace EpicRoadTrip.Test.Domain.Routes;

[TestClass]
public class RouteTest
{
    private const double Distance = 100.5;
    private const int CityOneId = 1;
    private const int CityTwoId = 2;
    private const int RoadtripId = 1;

    private static readonly TimeSpan Duration = TimeSpan.FromHours(1);

    [TestMethod]
    public void CreateRoute_WithValidData_ShouldCreateRoute()
    {
        // Act
        var route = Route.Create(Distance, Duration, CityOneId, CityTwoId, RoadtripId);

        // Assert
        Assert.AreEqual(Distance, route.Value.Distance);
        Assert.AreEqual(Duration, route.Value.Duration);
        Assert.AreEqual(CityOneId, route.Value.CityOneId);
        Assert.AreEqual(CityTwoId, route.Value.CityTwoId);
        Assert.AreEqual(RoadtripId, route.Value.RoadtripId);
    }

    [TestMethod]
    public void CreateRoute_WithNegativeDistance_ShouldThrowException()
    {
        // Arrange
        const double distance = -1;

        // Act
        var result = Route.Create(distance, Duration, CityOneId, CityTwoId, RoadtripId);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }

    [TestMethod]
    public void CreateRoute_WithNegativeDuration_ShouldThrowException()
    {
        // Arrange
        var duration = TimeSpan.FromHours(-1);

        // Act
        var result = Route.Create(Distance, duration, CityOneId, CityTwoId, RoadtripId);
    
        // Assert
        Assert.IsTrue(result.IsFailure);
    }

    [TestMethod]
    public void CreateRoute_WithSameCityIds_ShouldThrowException()
    {
        // Arrange
        const int cityTwoId = 1;

        // Act
        var result = Route.Create(Distance, Duration, CityOneId, cityTwoId, RoadtripId);
    
        // Assert
        Assert.IsTrue(result.IsFailure);
    }
}