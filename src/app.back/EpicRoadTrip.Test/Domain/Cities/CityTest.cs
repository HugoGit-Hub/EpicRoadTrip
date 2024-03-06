using EpicRoadTrip.Domain.Cities;
using EpicRoadTrip.Domain.Cities.Exceptions;

namespace EpicRoadTrip.Test.Domain.Cities;

[TestClass]
public class CityTest
{
    private const string Name = "Paris";

    [TestMethod]
    public void CreateCity_WithValidParameters_CreatesCity()
    {
        // Act
        var city = City.Create(Name);

        // Assert
        Assert.AreEqual(Name, city.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(CityInvalidNameException))]
    public void CreateCity_WithInvalidName_ThrowException()
    {
        // Arrange
        var invalidName = string.Empty;

        // Act
        City.Create(invalidName);
    }
}