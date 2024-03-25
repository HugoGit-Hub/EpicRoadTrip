using EpicRoadTrip.Application.Institutions.GetAllInstitution;
using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Institutions;
using Moq;

namespace EpicRoadTrip.Test.Application.Institutions;

[TestClass]
public class GetAllInstitutionQueryHandlerTest
{
    [TestMethod]
    public async Task Handle_ShouldReturnFailureResult_WhenRepositoryReturnsFailure()
    {
        // Arrange
        var mockRepository = new Mock<IRepository<Institution>>();
        mockRepository.Setup(r => r.GetAll())
            .Returns(Result<IEnumerable<Institution>>.Failure(RepositoryErrors.FailToGetAllError(It.IsAny<Exception>())));

        var handler = new GetAllInstitutionQueryHandler(mockRepository.Object);

        // Act
        var result = await handler.Handle(new GetAllInstitutionQuery(), CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
        Assert.AreEqual(RepositoryErrors.FailToGetAllError(It.IsAny<Exception>()), result.Error);
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
        const int roadTripId = 1;
        const string website = "";
        Tuple<float, float> coord = new Tuple<float, float>(1.1f, 1.1f);

    var mockRepository = new Mock<IRepository<Institution>>();
        var institution = Institution.Create(
            id,
            name,
            price,
            phoneNumber,
            email,
            address,
            type,
            roadTripId,
            website,
            coord.Item1,
            coord.Item2, null); ;
        var institutions = new List<Institution> { institution.Value };
        mockRepository.Setup(r => r.GetAll())
            .Returns(Result<IEnumerable<Institution>>.Success(institutions));

        var handler = new GetAllInstitutionQueryHandler(mockRepository.Object);

        // Act
        var result = await handler.Handle(new GetAllInstitutionQuery(), CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(institutions.Count, result.Value.Count());
    }
}