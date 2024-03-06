using EpicRoadTrip.Domain.Events;
using EpicRoadTrip.Domain.Institutions.Exceptions;

namespace EpicRoadTrip.Test.Domain.Events;

[TestClass]
public class EventTest
{
    private const string Name = "Event";
    private const double Price = 100.0;
    private const string PhoneNumber = "123456789";
    private const string Email = "example@gmail.com";
    private const string Address = "10 Rue de Rivoli";
    private const int CityId = 1;

    [TestMethod]
    public void CreateEvent_ValidParameters_CreatesEvent()
    {
        // Act
        var eventEntity = Event.Create(Name, Price, PhoneNumber, Email, Address, CityId);

        // Assert
        Assert.AreEqual(Name, eventEntity.Name);
        Assert.AreEqual(Price, eventEntity.Price);
        Assert.AreEqual(PhoneNumber, eventEntity.PhoneNumber);
        Assert.AreEqual(Email, eventEntity.Email);
        Assert.AreEqual(Address, eventEntity.Address);
        Assert.AreEqual(CityId, eventEntity.CityId);
    }

    [TestMethod]
    [ExpectedException(typeof(InstitutionInvalidNameException))]
    public void CreateEvent_WithInvalidName_ThrowException()
    {
        // Arrange
        var invalidName = string.Empty;

        // Act
        Event.Create(invalidName, Price, PhoneNumber, Email, Address, CityId);
    }

    [TestMethod]
    [ExpectedException(typeof(InstitutionInvalidAddressException))]
    public void CreateEvent_WithInvalidAddress_ThrowException()
    {
        // Arrange
        var invalidAddress = string.Empty;

        // Act
        Event.Create(Name, Price, PhoneNumber, Email, invalidAddress, CityId);
    }
}