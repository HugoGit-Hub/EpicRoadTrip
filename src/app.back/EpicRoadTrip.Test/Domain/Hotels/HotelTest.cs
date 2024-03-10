using EpicRoadTrip.Domain.Hotels;

namespace EpicRoadTrip.Test.Domain.Hotels;

[TestClass]
public sealed class HotelTest
{
    private const int Id = 1;
    private const string Name = "Hotel";
    private const double Price = 100.0;
    private const string PhoneNumber = "123456789";
    private const string Email = "example@gmail.com";
    private const string Address = "10 Rue de Rivoli";
    private const int CityId = 1;

    [TestMethod]
    public void CreateHotel_ValidParameters_CreatesHotel()
    {
        // Act
        var hotel = Hotel.Create(Id, Name, Price, PhoneNumber, Email, Address, CityId);

        // Assert
        Assert.AreEqual(Name, hotel.Value.Name);
        Assert.AreEqual(Price, hotel.Value.Price);
        Assert.AreEqual(PhoneNumber, hotel.Value.PhoneNumber);
        Assert.AreEqual(Email, hotel.Value.Email);
        Assert.AreEqual(Address, hotel.Value.Address);
        Assert.AreEqual(CityId, hotel.Value.CityId);
    }

    [TestMethod]
    public void CreateHotel_WithInvalidName_ThrowException()
    {
        // Arrange
        var invalidName = string.Empty;

        // Act
        var result = Hotel.Create(Id, invalidName, Price, PhoneNumber, Email, Address, CityId);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }

    [TestMethod]
    public void CreateHotel_WithInvalidAddress_ThrowException()
    {
        // Arrange
        var invalidAddress = string.Empty;

        // Act
        var result = Hotel.Create(Id, Name, Price, PhoneNumber, Email, invalidAddress, CityId);
    
        // Assert
        Assert.IsTrue(result.IsFailure);
    }
}