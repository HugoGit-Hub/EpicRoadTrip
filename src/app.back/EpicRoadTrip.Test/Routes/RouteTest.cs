using EpicRoadTrip.Domain.Routes;
using EpicRoadTrip.Domain.Routes.Exceptions;

namespace EpicRoadTrip.Test.Routes;

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
        Assert.AreEqual(Distance, route.Distance);
        Assert.AreEqual(Duration, route.Duration);
        Assert.AreEqual(CityOneId, route.CityOneId);
        Assert.AreEqual(CityTwoId, route.CityTwoId);
        Assert.AreEqual(RoadtripId, route.RoadtripId);
    }

    [TestMethod]
    [ExpectedException(typeof(RouteInvalidDistanceException))]
    public void CreateRoute_WithNegativeDistance_ShouldThrowException()
    {
        // Arrange
        const double distance = -1;

        // Act
        Route.Create(distance, Duration, CityOneId, CityTwoId, RoadtripId);
    }

    [TestMethod]
    [ExpectedException(typeof(RouteInvalidDurationException))]
    public void CreateRoute_WithNegativeDuration_ShouldThrowException()
    {
        // Arrange
        var duration = TimeSpan.FromHours(-1);

        // Act
        Route.Create(Distance, duration, CityOneId, CityTwoId, RoadtripId);
    }

    [TestMethod]
    [ExpectedException(typeof(RouteInvalidCitiesException))]
    public void CreateRoute_WithSameCityIds_ShouldThrowException()
    {
        // Arrange
        const int cityTwoId = 1;

        // Act
        Route.Create(Distance, Duration, CityOneId, cityTwoId, RoadtripId);
    }
}