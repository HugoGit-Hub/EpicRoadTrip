using EpicRoadTrip.Domain.Cities;

namespace EpicRoadTrip.Test.Domain.Cities;

[TestClass]
public class CityTest
{
    private const int Id = 1;
    private const string Name = "Paris";

    [TestMethod]
    public void CreateCity_WithValidParameters_CreatesCity()
    {
        // Act
        var city = City.Create(Id, Name);

        // Assert
        Assert.AreEqual(Name, city.Value.Name);
    }

    [TestMethod]
    public void CreateCity_WithInvalidName_ThrowException()
    {
        // Arrange
        var invalidName = string.Empty;

        // Act
        var result = City.Create(Id, invalidName);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }
}