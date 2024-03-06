using EpicRoadTrip.Domain.Transportations;
using EpicRoadTrip.Domain.Transportations.Exceptions;

namespace EpicRoadTrip.Test.Transportations;

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
        Assert.AreEqual(Score, transportation.Score);
        Assert.AreEqual(Company, transportation.Company);
        Assert.AreEqual(Address, transportation.Address);
        Assert.AreEqual(Type, transportation.TransportationType);
    }

    [TestMethod]
    [DataRow(5.1)]
    [DataRow(-2)]
    [ExpectedException(typeof(TransportationInvalidScoreException))]
    public void CreateTransportation_WithInvalidScore_ShouldThrowTransportationInvalidScoreException(double invalidScore)
    {
        // Act
        Transportation.Create(invalidScore, Company, Address, Type);
    }

    [TestMethod]
    [ExpectedException(typeof(TransportationInvalidCompanyException))]
    public void CreateTransportation_WithInvalidCompany_ShouldThrowTransportationInvalidCompanyException()
    {
        // Arrange
        var invalidCompany = string.Empty;

        // Act
        Transportation.Create(Score, invalidCompany, Address, Type);
    }

    [TestMethod]
    [ExpectedException(typeof(TransportationInvalidAddressException))]
    public void CreateTransportation_WithInvalidAddress_ShouldThrowTransportationInvalidAddressException()
    {
        // Arrange
        var invalidAddress = string.Empty;

        // Act
        Transportation.Create(Score, Company, invalidAddress, Type);
    }
}