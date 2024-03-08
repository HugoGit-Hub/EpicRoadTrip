using EpicRoadTrip.Domain.Restaurants;

namespace EpicRoadTrip.Test.Domain.Restaurants;

[TestClass]
public class RestaurantTest
{
    private const string Name = "Restaurant";
    private const double Price = 100.0;
    private const string PhoneNumber = "123456789";
    private const string Email = "example@gmail.com";
    private const string Address = "10 Rue de Rivoli";
    private const int CityId = 1;

    [TestMethod]
    public void CreateRestaurant_ValidParameters_CreatesRestaurant()
    {
        // Act
        var hotel = Restaurant.Create(Name, Price, PhoneNumber, Email, Address, CityId);

        // Assert
        Assert.AreEqual(Name, hotel.Value.Name);
        Assert.AreEqual(Price, hotel.Value.Price);
        Assert.AreEqual(PhoneNumber, hotel.Value.PhoneNumber);
        Assert.AreEqual(Email, hotel.Value.Email);
        Assert.AreEqual(Address, hotel.Value.Address);
        Assert.AreEqual(CityId, hotel.Value.CityId);
    }

    [TestMethod]
    public void CreateRestaurant_WithInvalidName_ThrowException()
    {
        // Arrange
        var invalidName = string.Empty;

        // Act
        var result = Restaurant.Create(invalidName, Price, PhoneNumber, Email, Address, CityId);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }

    [TestMethod]
    public void CreateRestaurant_WithInvalidAddress_ThrowException()
    {
        // Arrange
        var invalidAddress = string.Empty;

        // Act
        var result = Restaurant.Create(Name, Price, PhoneNumber, Email, invalidAddress, CityId);
    
        // Assert
        Assert.IsTrue(result.IsFailure);
    }
}