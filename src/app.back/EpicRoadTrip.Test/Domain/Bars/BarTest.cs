using EpicRoadTrip.Domain.Bars;

namespace EpicRoadTrip.Test.Domain.Bars;

[TestClass]
public class BarTest
{
    private const string Name = "Bar";
    private const double Price = 100.0;
    private const string PhoneNumber = "123456789";
    private const string Email = "example@gmail.com";
    private const string Address = "10 Rue de Rivoli";
    private const int CityId = 1;

    [TestMethod]
    public void CreateBar_ValidParameters_CreatesBar()
    {
        // Act
        var bar = Bar.Create(Name, Price, PhoneNumber, Email, Address, CityId);

        // Assert
        Assert.AreEqual(Name, bar.Value.Name);
        Assert.AreEqual(Price, bar.Value.Price);
        Assert.AreEqual(PhoneNumber, bar.Value.PhoneNumber);
        Assert.AreEqual(Email, bar.Value.Email);
        Assert.AreEqual(Address, bar.Value.Address);
        Assert.AreEqual(CityId, bar.Value.CityId);
    }

    [TestMethod]
    public void CreateBar_WithInvalidName_ThrowException()
    {
        // Arrange
        var invalidName = string.Empty;

        // Act
        var result = Bar.Create(invalidName, Price, PhoneNumber, Email, Address, CityId);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }

    [TestMethod]
    public void CreateBar_WithInvalidAddress_ThrowException()
    {
        // Arrange
        var invalidAddress = string.Empty;

        // Act
        var result = Bar.Create(Name, Price, PhoneNumber, Email, invalidAddress, CityId);
    
        // Assert
        Assert.IsTrue(result.IsFailure);
    }
}