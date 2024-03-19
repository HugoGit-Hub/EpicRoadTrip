using EpicRoadTrip.Domain.Roadtrips;

namespace EpicRoadTrip.Test.Domain.Roadtrips;

[TestClass]
public class RoadtripTest
{
    private const double PositiveBudget = 1000.0;
    private const int NbrTransfere = 0;
    private const string Co2Emission = "";

    private readonly DateTime _dateTimeNow = DateTime.Now;
    private readonly TimeSpan _timeSpan = new(7, 0, 0, 0);
    private readonly IEnumerable<string> _tags = ["tag1", "tag2"];

    [TestMethod]
    public void CreateRoadtrip_ValidParameters_CreatesRoadtrip()
    {
        // Arrange
        const int userId = 1;
        DateTime? endDate = DateTime.Now.AddDays(7);

        // Act
        var roadtrip = Roadtrip.Create(
            1, 
            PositiveBudget,
            _dateTimeNow,
            endDate,
            userId,
            _timeSpan,
            NbrTransfere,
            _tags,
            Co2Emission);

        // Assert
        Assert.AreEqual(PositiveBudget, roadtrip.Value.Budget);
        Assert.AreEqual(_dateTimeNow, roadtrip.Value.StartDate);
        Assert.AreEqual(endDate, roadtrip.Value.EndDate);
        Assert.AreEqual(userId, roadtrip.Value.UserId);
    }

    [TestMethod]
    public void CreateRoadtrip_InvalidBudget_ThrowsException()
    {
        // Arrange
        const double budget = -1000.0;
        DateTime? endDate = DateTime.Now.AddDays(7);
        const int userId = 1;

        // Act
        var result =Roadtrip.Create(
            1,
            budget, 
            _dateTimeNow, 
            endDate, 
            userId,
            _timeSpan,
            NbrTransfere,
            _tags,
            Co2Emission);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }

    [TestMethod]
    public void CreateRoadtrip_InvalidDates_ThrowsException()
    {
        // Arrange
        var startDate = DateTime.Now.AddDays(7);
        const int userId = 1;

        // Act
        var result =Roadtrip.Create(
            1,
            PositiveBudget,
            startDate,
            _dateTimeNow, 
            userId,
            _timeSpan,
            NbrTransfere,
            _tags,
            Co2Emission);
    
        // Assert
        Assert.IsTrue(result.IsFailure);
    }

}