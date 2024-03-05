using EpicRoadTrip.Domain.Bars;
using EpicRoadTrip.Domain.Institutions.Exceptions;

namespace EpicRoadTrip.Test.Bars;

[TestClass]
public class BarTest
{
    private const int Id = 1;
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
        var bar = Bar.Create(Id, Name, Price, PhoneNumber, Email, Address, CityId);

        // Assert
        Assert.AreEqual(Id, bar.Id);
        Assert.AreEqual(Name, bar.Name);
        Assert.AreEqual(Price, bar.Price);
        Assert.AreEqual(PhoneNumber, bar.PhoneNumber);
        Assert.AreEqual(Email, bar.Email);
        Assert.AreEqual(Address, bar.Address);
        Assert.AreEqual(CityId, bar.CityId);
    }

    [TestMethod]
    [ExpectedException(typeof(InstitutionInvalidNameException))]
    public void CreateBar_WithInvalidName_ThrowException()
    {
        // Arrange
        var invalidName = string.Empty;

        // Act
        Bar.Create(Id, invalidName, Price, PhoneNumber, Email, Address, CityId);
    }

    [TestMethod]
    [ExpectedException(typeof(InstitutionInvalidAddressException))]
    public void CreateBar_WithInvalidAddress_ThrowException()
    {
        // Arrange
        var invalidAddress = string.Empty;

        // Act
        Bar.Create(Id, Name, Price, PhoneNumber, Email, invalidAddress, CityId);
    }
}