using EpicRoadTrip.Domain.Institutions;

namespace EpicRoadTrip.Test.Domain.Institutions;

[TestClass]
public class InstitutionTest
{
    private const int Id = 1;
    private const string Name = "Test";
    private const double Price = 10.0;
    private const string PhoneNumber = "123456789";
    private const string Email = "test@example.com";
    private const string Address = "Test";
    private const InstitutionType Type = InstitutionType.Hotel;
    private const int CityId = 1;

    [TestMethod]
    public void Create_ShouldReturnInstitution_WithGoodParameters()
    {
        // Act
        var result = Institution.Create(Id, Name, Price, PhoneNumber, Email, Address, Type, CityId);

        // Assert
        Assert.IsTrue(result.IsSuccess);
    }

    [TestMethod]
    public void Create_ShouldThrowInstitutionInvalidNameException_WithEmptyName()
    {
        // Arrange
        const string name = "";

        // Act
        var result = Institution.Create(Id, name, Price, PhoneNumber, Email, Address, Type, CityId);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }

    [TestMethod]
    public void Create_ShouldThrowInstitutionInvalidAddressException_WithEmptyAddress()
    {
        // Arrange
        const string address = "";

        // Act
        var result = Institution.Create(Id, Name, Price, PhoneNumber, Email, address, Type, CityId);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }
}