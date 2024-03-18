using EpicRoadTrip.Application.Cities.CreateCity;
using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.Cities;
using EpicRoadTrip.Domain.Cities.Exceptions;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.ErrorHandling.Generics;
using Moq;

namespace EpicRoadTrip.Test.Application.Cities;

[TestClass]
public class CreateCityCommandHandlerTest
{
    private Mock<IRepository<City>> mockRepository;
    private CreateCityCommandHandler createCityCommandHandler;

    [TestInitialize]
    public void TestInitialize()
    {
        mockRepository = new Mock<IRepository<City>>();
        createCityCommandHandler = new CreateCityCommandHandler(mockRepository.Object);
    }

    [TestMethod]
    public async Task Handle_ReturnsFailure_WhenCityCreationFails()
    {
        // Arrange
        var command = new CreateCityCommand(null);

        // Act
        var result = await createCityCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
        Assert.AreEqual(GenericErrors<City>.InvalidFormatError(new CityInvalidNameException()), result.Error);
    }

    [TestMethod]
    public async Task Handle_ReturnsFailure_WhenCreateFails()
    {
        // Arrange
        var command = new CreateCityCommand("Test");
        mockRepository
            .Setup(x => x.Create(It.IsAny<City>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<City>.Failure(RepositoryErrors.FailedToCreateError(It.IsAny<Exception>())));

        // Act
        var result = await createCityCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
        Assert.AreEqual(RepositoryErrors.FailedToCreateError(It.IsAny<Exception>()), result.Error);
    }

    [TestMethod]
    public async Task Handle_ReturnsSuccess_WhenCreateSucceeds()
    {
        // Arrange
        var command = new CreateCityCommand("Test");
        var city = City.Create(0, command.Name).Value;
        mockRepository
            .Setup(x => x.Create(It.IsAny<City>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<City>.Success(city));

        // Act
        var result = await createCityCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(city.Id, result.Value.Id);
        Assert.AreEqual(city.Name, result.Value.Name);
    }
}