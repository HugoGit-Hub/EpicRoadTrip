using EpicRoadTrip.Domain.Institutions.Exceptions;
using EpicRoadTrip.Domain.Restaurants;

namespace EpicRoadTrip.Test.Restaurants;

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
        Assert.AreEqual(Name, hotel.Name);
        Assert.AreEqual(Price, hotel.Price);
        Assert.AreEqual(PhoneNumber, hotel.PhoneNumber);
        Assert.AreEqual(Email, hotel.Email);
        Assert.AreEqual(Address, hotel.Address);
        Assert.AreEqual(CityId, hotel.CityId);
    }

    [TestMethod]
    [ExpectedException(typeof(InstitutionInvalidNameException))]
    public void CreateRestaurant_WithInvalidName_ThrowException()
    {
        // Arrange
        var invalidName = string.Empty;

        // Act
        Restaurant.Create(invalidName, Price, PhoneNumber, Email, Address, CityId);
    }

    [TestMethod]
    [ExpectedException(typeof(InstitutionInvalidAddressException))]
    public void CreateRestaurant_WithInvalidAddress_ThrowException()
    {
        // Arrange
        var invalidAddress = string.Empty;

        // Act
        Restaurant.Create(Name, Price, PhoneNumber, Email, invalidAddress, CityId);
    }
}