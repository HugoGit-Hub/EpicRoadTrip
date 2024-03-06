using EpicRoadTrip.Domain.Roadtrips.Exceptions;
using EpicRoadTrip.Domain.Roadtrips;

namespace EpicRoadTrip.Test.Domain.Roadtrips;

[TestClass]
public class RoadtripTest
{
    private const double PositiveBudget = 1000.0;
    private readonly DateTime _dateTimeNow = DateTime.Now;

    [TestMethod]
    public void CreateRoadtrip_ValidParameters_CreatesRoadtrip()
    {
        // Arrange
        const int userId = 1;
        DateTime? endDate = DateTime.Now.AddDays(7);

        // Act
        var roadtrip = Roadtrip.Create(PositiveBudget, _dateTimeNow, endDate, userId);

        // Assert
        Assert.AreEqual(PositiveBudget, roadtrip.Budget);
        Assert.AreEqual(_dateTimeNow, roadtrip.StartDate);
        Assert.AreEqual(endDate, roadtrip.EndDate);
        Assert.AreEqual(userId, roadtrip.UserId);
    }

    [TestMethod]
    [ExpectedException(typeof(RoadtripNotValidBudgetException))]
    public void CreateRoadtrip_InvalidBudget_ThrowsException()
    {
        // Arrange
        const double budget = -1000.0;
        DateTime? endDate = DateTime.Now.AddDays(7);
        const int userId = 1;

        // Act
        Roadtrip.Create(budget, _dateTimeNow, endDate, userId);
    }

    [TestMethod]
    [ExpectedException(typeof(RoadtripNotValidDatesException))]
    public void CreateRoadtrip_InvalidDates_ThrowsException()
    {
        // Arrange
        var startDate = DateTime.Now.AddDays(7);
        const int userId = 1;

        // Act
        Roadtrip.Create(PositiveBudget, startDate, _dateTimeNow, userId);
    }

}