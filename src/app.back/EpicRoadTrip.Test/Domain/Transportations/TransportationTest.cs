using EpicRoadTrip.Domain.Transportations;

namespace EpicRoadTrip.Test.Domain.Transportations;

[TestClass]
public class TransportationTest
{
    private const double Score = 4.5;
    private const string Company = "Company";
    private const string Address = "Address";
    private const TransportationType Type = TransportationType.Airplane;

    [TestMethod]
    public void CreateTransportation_WithValidParameters_ShouldReturnTransportation()
    {
        // Act
        var transportation = Transportation.Create(Score, Company, Address, Type);

        // Assert
        Assert.IsNotNull(transportation);
        Assert.AreEqual(Score, transportation.Value.Score);
        Assert.AreEqual(Company, transportation.Value.Company);
        Assert.AreEqual(Address, transportation.Value.Address);
        Assert.AreEqual(Type, transportation.Value.TransportationType);
    }

    [TestMethod]
    [DataRow(5.1)]
    [DataRow(-2)]
    public void CreateTransportation_WithInvalidScore_ShouldThrowTransportationInvalidScoreException(double invalidScore)
    {
        // Act
        var result = Transportation.Create(invalidScore, Company, Address, Type);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }

    [TestMethod]
    public void CreateTransportation_WithInvalidCompany_ShouldThrowTransportationInvalidCompanyException()
    {
        // Arrange
        var invalidCompany = string.Empty;

        // Act
        var result = Transportation.Create(Score, invalidCompany, Address, Type);


        // Assert
        Assert.IsTrue(result.IsFailure);
    }

    [TestMethod]
    public void CreateTransportation_WithInvalidAddress_ShouldThrowTransportationInvalidAddressException()
    {
        // Arrange
        var invalidAddress = string.Empty;

        // Act
        var result = Transportation.Create(Score, Company, invalidAddress, Type);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }
}