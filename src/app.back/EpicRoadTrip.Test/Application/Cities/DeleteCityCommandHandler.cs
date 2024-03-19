using EpicRoadTrip.Application.Cities.DeleteCity;
using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.Cities;
using EpicRoadTrip.Domain.ErrorHandling;
using Moq;

namespace EpicRoadTrip.Test.Application.Cities;

[TestClass]
public class DeleteCityCommandHandlerTest
{
    private const int CityId = 1;

    private Mock<IRepository<City>> mockRepository;
    private DeleteCityCommandHandler deleteCityCommandHandler;

    [TestInitialize]
    public void TestInitialize()
    {
        mockRepository = new Mock<IRepository<City>>();
        deleteCityCommandHandler = new DeleteCityCommandHandler(mockRepository.Object);
    }

    [TestMethod]
    public async Task Handle_ReturnsSuccess_WhenDeleteSucceeds()
    {
        // Arrange
        var city = City.Create(1, "Test").Value;
        mockRepository
            .Setup(x => x.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<City>.Success(city));
        mockRepository
            .Setup(x => x.Delete(It.IsAny<City>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<City>.Success(city));

        // Act
        var result = await deleteCityCommandHandler.Handle(new DeleteCityCommand(CityId), CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(city.Id, result.Value.Id);
        Assert.AreEqual(city.Name, result.Value.Name);
    }

    [TestMethod]
    public async Task Handle_ReturnsFailure_WhenGetByIdFails()
    {
        // Arrange
        mockRepository
            .Setup(x => x.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<City>.Failure(RepositoryErrors.FailedToGetByIdError()));

        // Act
        var result = await deleteCityCommandHandler.Handle(new DeleteCityCommand(CityId), CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
        Assert.AreEqual(RepositoryErrors.FailedToGetByIdError(), result.Error);
    }

    [TestMethod]
    public async Task Handle_ReturnsFailure_WhenDeleteFails()
    {
        // Arrange
        var city = City.Create(1, "Test").Value;
        mockRepository
            .Setup(x => x.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<City>.Success(city));
        mockRepository
            .Setup(x => x.Delete(It.IsAny<City>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<City>.Failure(RepositoryErrors.FailedToDeleteError(It.IsAny<Exception>())));

        // Act
        var result = await deleteCityCommandHandler.Handle(new DeleteCityCommand(CityId), CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
        Assert.AreEqual(RepositoryErrors.FailedToDeleteError(It.IsAny<Exception>()), result.Error);
    }
}