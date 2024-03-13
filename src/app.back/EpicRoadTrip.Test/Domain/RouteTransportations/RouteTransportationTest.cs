using EpicRoadTrip.Domain.RouteTransportations;

namespace EpicRoadTrip.Test.Domain.RouteTransportations;

[TestClass]
public class RouteTransportationTest
{
    private const int RouteId = 1;
    private const int TransportationId = 1;

    [TestMethod]
    public void CreateRouteTransportation_WithValidParameters_ShouldCreatsRouteTransportation()
    {
        // Act
        var routeTransportation = RouteTransportation.Create(RouteId, TransportationId);

        // Assert
        Assert.AreEqual(RouteId, routeTransportation.Value.RouteId);
        Assert.AreEqual(TransportationId, routeTransportation.Value.TransportationId);
    }
}