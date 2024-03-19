using EpicRoadTrip.Application.Cities.GetAllCity;
using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.Cities;
using EpicRoadTrip.Domain.ErrorHandling;
using Moq;

namespace EpicRoadTrip.Test.Application.Cities;

[TestClass]
public class GetAllCityQueryHandlerTest
{
    private Mock<IRepository<City>> mockRepository;
    private GetAllCityQueryHandler getAllCityQueryHandler;

    [TestInitialize]
    public void TestInitialize()
    {
        mockRepository = new Mock<IRepository<City>>();
        getAllCityQueryHandler = new GetAllCityQueryHandler(mockRepository.Object);
    }

    [TestMethod]
    public async Task Handle_ReturnsSuccess_WhenGetAllSucceeds()
    {
        // Arrange
        var cityOne = City.Create(1, "City One");
        var cityTwo = City.Create(2, "City Two");
        var cities = new List<City> { cityOne.Value, cityTwo.Value };
        mockRepository
            .Setup(x => x.GetAll())
            .Returns(Result<IEnumerable<City>>.Success(cities));

        // Act
        var result = await getAllCityQueryHandler.Handle(new GetAllCityQuery(), CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(cities.Count, result.Value.Count());
    }

    [TestMethod]
    public async Task Handle_ReturnsFailure_WhenGetAllFails()
    {
        // Arrange
        mockRepository
            .Setup(x => x.GetAll())
            .Returns(Result<IEnumerable<City>>.Failure(RepositoryErrors.FailToGetAllError(It.IsAny<Exception>())));

        // Act
        var result = await getAllCityQueryHandler.Handle(new GetAllCityQuery(), CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }
}