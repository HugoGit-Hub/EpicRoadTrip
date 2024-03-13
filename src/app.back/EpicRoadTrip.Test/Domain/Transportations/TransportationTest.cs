using EpicRoadTrip.Domain.Transportations;

namespace EpicRoadTrip.Test.Domain.Transportations;

[TestClass]
public class TransportationTest
{
    private const int Id = 1;
    private const double Cost = 100.0;
    private const double Score = 4.5;
    private const string Company = "Company";
    private const string Address = "Address";
    private const TransportationType Type = TransportationType.Airplane;

    [TestMethod]
    public void CreateTransportation_WithValidParameters_ShouldReturnTransportation()
    {
        // Act
        var transportation = Transportation.Create(Id, Cost, Score, Company, Address, Type);

        // Assert
        Assert.IsNotNull(transportation);
        Assert.AreEqual(Score, transportation.Value.Score);
        Assert.AreEqual(Company, transportation.Value.Company);
        Assert.AreEqual(Address, transportation.Value.Address);
        Assert.AreEqual(Type, transportation.Value.TransportationType);
    }

    [TestMethod]
    public void CreateRouteTransportation_WithInvalidCost_ShoudlThrowRouteTransportationInvalidCostException()
    {
        // Arrange
        const double invalidCost = -1.0;

        // Act
        var result = Transportation.Create(Id, invalidCost, Score, Company, Address, Type);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }

    [TestMethod]
    [DataRow(5.1)]
    [DataRow(-2)]
    public void CreateTransportation_WithInvalidScore_ShouldThrowTransportationInvalidScoreException(double invalidScore)
    {
        // Act
        var result = Transportation.Create(Id, Cost, invalidScore, Company, Address, Type);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }

    [TestMethod]
    public void CreateTransportation_WithInvalidCompany_ShouldThrowTransportationInvalidCompanyException()
    {
        // Arrange
        var invalidCompany = string.Empty;

        // Act
        var result = Transportation.Create(Id, Cost, Score, invalidCompany, Address, Type);


        // Assert
        Assert.IsTrue(result.IsFailure);
    }

    [TestMethod]
    public void CreateTransportation_WithInvalidAddress_ShouldThrowTransportationInvalidAddressException()
    {
        // Arrange
        var invalidAddress = string.Empty;

        // Act
        var result = Transportation.Create(Id, Cost, Score, Company, invalidAddress, Type);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }
}