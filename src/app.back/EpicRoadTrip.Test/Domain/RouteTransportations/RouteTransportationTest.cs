using EpicRoadTrip.Domain.RouteTransportations;
using EpicRoadTrip.Domain.RouteTransportations.Exceptions;

namespace EpicRoadTrip.Test.Domain.RouteTransportations;

[TestClass]
public class RouteTransportationTest
{
    private const double Cost = 100.0;
    private const int RouteId = 1;
    private const int TransportationId = 1;

    [TestMethod]
    public void CreateRouteTransportation_WithValidParameters_ShouldCreatsRouteTransportation()
    {
        // Act
        var routeTransportation = RouteTransportation.Create(Cost, RouteId, TransportationId);

        // Assert
        Assert.AreEqual(Cost, routeTransportation.Cost);
        Assert.AreEqual(RouteId, routeTransportation.RouteId);
        Assert.AreEqual(TransportationId, routeTransportation.TransportationId);
    }

    [TestMethod]
    [ExpectedException(typeof(RouteTransportationInvalidCostException))]
    public void CreateRouteTransportation_WithInvalidCost_ShoudlThrowRouteTransportationInvalidCostException()
    {
        // Arrange
        const double invalidCost = -1.0;

        // Act
        RouteTransportation.Create(invalidCost, RouteId, TransportationId);
    }
}