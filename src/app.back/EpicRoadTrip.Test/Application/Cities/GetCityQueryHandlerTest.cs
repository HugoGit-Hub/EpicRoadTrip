using EpicRoadTrip.Application.Cities.GetCity;
using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.Cities;
using EpicRoadTrip.Domain.ErrorHandling;
using Mapster;
using Moq;

namespace EpicRoadTrip.Test.Application.Cities;

[TestClass]
public class GetCityQueryHandlerTest
{
    private Mock<IRepository<City>> mockRepository;
    private GetCityQueryHandler getCityQueryHandler;

    [TestInitialize]
    public void TestInitialize()
    {
        mockRepository = new Mock<IRepository<City>>();
        getCityQueryHandler = new GetCityQueryHandler(mockRepository.Object);
    }

    [TestMethod]
    public async Task Handle_ReturnsSuccess_WhenGetByIdSucceeds()
    {
        // Arrange
        var city = City.Create(1, "string");
        mockRepository
            .Setup(x => x.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<City>.Success(city.Value));

        var query = new { Id = 1, Name = "string" }.Adapt<GetCityQuery>();

        // Act
        var result = await getCityQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(city.Value.Id, result.Value.Id);
        Assert.AreEqual(city.Value.Name, result.Value.Name);
    }

    [TestMethod]
    public async Task Handle_ReturnsFailure_WhenGetByIdFails()
    {
        // Arrange
        mockRepository
            .Setup(x => x.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<City>.Failure(RepositoryErrors.FailedToGetByIdError()));

        var query = new { Id = 1, Name = "string" }.Adapt<GetCityQuery>();

        // Act
        var result = await getCityQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
        Assert.AreEqual(RepositoryErrors.FailedToGetByIdError(), result.Error);
    }
}