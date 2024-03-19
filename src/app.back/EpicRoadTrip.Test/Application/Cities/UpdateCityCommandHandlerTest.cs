using Azure.Core;
using EpicRoadTrip.Application.Cities.UpdateCity;
using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.Cities;
using EpicRoadTrip.Domain.Cities.Exceptions;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.ErrorHandling.Generics;
using Mapster;
using Moq;

namespace EpicRoadTrip.Test.Application.Cities;

[TestClass]
public class UpdateCityCommandHandlerTest
{
    private Mock<IRepository<City>> mockRepository;
    private UpdateCityCommandHandler updateCityCommandHandler;

    [TestInitialize]
    public void TestInitialize()
    {
        mockRepository = new Mock<IRepository<City>>();
        updateCityCommandHandler = new UpdateCityCommandHandler(mockRepository.Object);
    }

    [TestMethod]
    public async Task Handle_ReturnsSuccess_WhenUpdateSucceeds()
    {
        // Arrange
        var request = new UpdateCityRequest { Id = 1, Name = "Test" };
        var command = new UpdateCityCommand(request);
        var city = City.Create(command.Request.Id, command.Request.Name).Value;
        mockRepository
            .Setup(x => x.Update(It.IsAny<City>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<City>.Success(city));

        // Act
        var result = await updateCityCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(city.Id, result.Value.Id);
        Assert.AreEqual(city.Name, result.Value.Name);
    }
    
    [TestMethod]
    public async Task Handle_ReturnsFailure_WhenCityCreationFails()
    {
        // Arrange
        var request = new UpdateCityRequest { Id = 1, Name = null };
        var command = new UpdateCityCommand(request);

        // Act
        var result = await updateCityCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
        Assert.AreEqual(GenericErrors<City>.InvalidFormatError(new CityInvalidNameException()), result.Error);
    }

    [TestMethod]
    public async Task Handle_ReturnsFailure_WhenUpdateFails()
    {
        // Arrange
        var request = new UpdateCityRequest { Id = 1, Name = "Test" };
        var command = new UpdateCityCommand(request);
        mockRepository
            .Setup(x => x.Update(It.IsAny<City>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<City>.Failure(RepositoryErrors.FailedToUpdateError(It.IsAny<Exception>())));

        // Act
        var result = await updateCityCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
        Assert.AreEqual(RepositoryErrors.FailedToUpdateError(It.IsAny<Exception>()), result.Error);
    }
}