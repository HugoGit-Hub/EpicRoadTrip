using EpicRoadTrip.Application.Institutions.GetInstitution;
using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Institutions;
using Moq;

namespace EpicRoadTrip.Test.Application.Institutions;

[TestClass]
public class GetInstitutionQueryHandlerTest
{
    private const int Id = 1;

    [TestMethod]
    public async Task Handle_ShouldReturnFailureResult_WhenRepositoryReturnsFailure()
    {
        // Arrange
        var mockRepository = new Mock<IRepository<Institution>>();
        var query = new GetInstitutionQuery(Id);
        mockRepository.Setup(r => r.GetById(query.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Institution>.Failure(RepositoryErrors.FailedToGetByIdError()));

        var handler = new GetInstitutionQueryHandler(mockRepository.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
        Assert.AreEqual(RepositoryErrors.FailedToGetByIdError(), result.Error);
    }

    [TestMethod]
    public async Task Handle_ShouldReturnSuccessResult_WhenRepositoryReturnsSuccess()
    {
        // Arrange
        const int id = 1;
        const string name = "Institution";
        const double price = 100.2;
        const string phoneNumber = "123456789";
        const string email = "test@gmail.com";
        const string address = "Test address";
        const InstitutionType type = InstitutionType.Hotel;
        const int cityId = 1;
        var mockRepository = new Mock<IRepository<Institution>>();
        var query = new GetInstitutionQuery(id);
        var institution = Institution.Create(
            id,
            name,
            price,
            phoneNumber,
            email,
            address,
            type,
            cityId);
        mockRepository.Setup(r => r.GetById(query.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Institution>.Success(institution.Value));

        var handler = new GetInstitutionQueryHandler(mockRepository.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.IsNotNull(result.Value);
    }
}