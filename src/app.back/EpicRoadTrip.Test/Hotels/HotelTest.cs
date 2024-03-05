using EpicRoadTrip.Domain.Hotels;
using EpicRoadTrip.Domain.Institutions.Exceptions;

namespace EpicRoadTrip.Test.Hotels;

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
        Assert.AreEqual(Id, hotel.Id);
        Assert.AreEqual(Name, hotel.Name);
        Assert.AreEqual(Price, hotel.Price);
        Assert.AreEqual(PhoneNumber, hotel.PhoneNumber);
        Assert.AreEqual(Email, hotel.Email);
        Assert.AreEqual(Address, hotel.Address);
        Assert.AreEqual(CityId, hotel.CityId);
    }

    [TestMethod]
    [ExpectedException(typeof(InstitutionInvalidNameException))]
    public void CreateHotel_WithInvalidName_ThrowException()
    {
        // Arrange
        var invalidName = string.Empty;

        // Act
        Hotel.Create(Id, invalidName, Price, PhoneNumber, Email, Address, CityId);
    }

    [TestMethod]
    [ExpectedException(typeof(InstitutionInvalidAddressException))]
    public void CreateHotel_WithInvalidAddress_ThrowException()
    {
        // Arrange
        var invalidAddress = string.Empty;

        // Act
        Hotel.Create(Id, Name, Price, PhoneNumber, Email, invalidAddress, CityId);
    }
}