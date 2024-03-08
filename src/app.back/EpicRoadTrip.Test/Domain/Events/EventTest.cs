using EpicRoadTrip.Domain.Events;

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
        Assert.AreEqual(Name, eventEntity.Value.Name);
        Assert.AreEqual(Price, eventEntity.Value.Price);
        Assert.AreEqual(PhoneNumber, eventEntity.Value.PhoneNumber);
        Assert.AreEqual(Email, eventEntity.Value.Email);
        Assert.AreEqual(Address, eventEntity.Value.Address);
        Assert.AreEqual(CityId, eventEntity.Value.CityId);
    }

    [TestMethod]
    public void CreateEvent_WithInvalidName_ThrowException()
    {
        // Arrange
        var invalidName = string.Empty;

        // Act
        var result = Event.Create(invalidName, Price, PhoneNumber, Email, Address, CityId);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }

    [TestMethod]
    public void CreateEvent_WithInvalidAddress_ThrowException()
    {
        // Arrange
        var invalidAddress = string.Empty;

        // Act
        var result = Event.Create(Name, Price, PhoneNumber, Email, invalidAddress, CityId);
    
        // Assert
        Assert.IsTrue(result.IsFailure);
    }
}