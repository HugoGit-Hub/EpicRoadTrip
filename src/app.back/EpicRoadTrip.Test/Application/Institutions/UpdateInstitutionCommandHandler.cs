using EpicRoadTrip.Application.Institutions.UpdateInstitution;
using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Institutions;
using EpicRoadTrip.Domain.Roadtrips;
using Mapster;
using Moq;
using System.Drawing;

namespace EpicRoadTrip.Test.Application.Institutions;

[TestClass]
public class UpdateInstitutionCommandHandlerTest
{
    [TestMethod]
    public async Task Handle_ShouldReturnFailureResult_WhenRepositoryReturnsFailure()
    {
        // Arrange
        var mockRepository = new Mock<IRepository<Institution>>();
        var request = new
        {
            Id = 1,
            Name = "Institution",
            Price = 100.0,
            PhoneNumber = "123456789",
            Email = "test@gmail.com",
            Address = "Address",
            Type = InstitutionType.Hotel,
            CityId = 1
        };
        var command = new UpdateInstitutionCommand(request.Adapt<UpdateInstitutionRequest>());
        mockRepository.Setup(r => r.Update(It.IsAny<Institution>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Institution>.Failure(RepositoryErrors.FailedToUpdateError(It.IsAny<Exception>())));

        var handler = new UpdateInstitutionCommandHandler(mockRepository.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
        Assert.AreEqual(RepositoryErrors.FailedToUpdateError(It.IsAny<Exception>()), result.Error);
    }

    [TestMethod]
    public async Task Handle_ShouldReturnSuccessResult_WhenRepositoryReturnsSuccess()
    {
        // Arrange
        var mockRepository = new Mock<IRepository<Institution>>();
        var request = new
        {
            Id = 1,
            Name = "Institution",
            Price = 100.0,
            PhoneNumber = "123456789",
            Email = "test@gmail.com",
            Address = "Address",
            Type = InstitutionType.Hotel,
            RoadTripId = 1,
            Website = "",
            Coord = new Tuple<float, float>(1.1f, 1.1f)
        };

        var command = new UpdateInstitutionCommand(request.Adapt<UpdateInstitutionRequest>());
        var institution = Institution.Create(
            1,
            request.Name,
            request.Price,
            request.PhoneNumber,
            request.Email,
            request.Address,
            request.Type, 
            request.RoadTripId,
            request.Website,
            request.Coord);
        mockRepository.Setup(r => r.Update(It.IsAny<Institution>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Institution>.Success(institution.Value));

        var handler = new UpdateInstitutionCommandHandler(mockRepository.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.IsNotNull(result.Value);
    }
}
