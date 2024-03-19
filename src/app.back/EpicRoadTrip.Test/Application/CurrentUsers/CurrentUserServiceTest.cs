using EpicRoadTrip.Application.CurrentUsers;
using EpicRoadTrip.Application.Users;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Users;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace EpicRoadTrip.Test.Application.CurrentUsers;

[TestClass]
public class CurrentUserServiceTest(CurrentUserService currentUserService)
{
    private Mock<IHttpContextAccessor> _mockHttpContextAccessor = new();
    private Mock<IUserRepository> _mockUserRepository = new();

    [TestInitialize]
    public void TestInitialize()
    {
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _mockUserRepository = new Mock<IUserRepository>();
        currentUserService = new CurrentUserService(_mockHttpContextAccessor.Object, _mockUserRepository.Object);
    }

    [TestMethod]
    public async Task GetCurrentUser_ReturnsSuccess()
    {
        // Arrange
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, "test@example.com")
        };
        _mockHttpContextAccessor
            .Setup(x => x.HttpContext!.User.Claims)
            .Returns(claims);

        var user = User.Create(
            1,
            "string",
            "string",
            "string",
            "string",
            25,
            true);
        _mockUserRepository
            .Setup(x => x.GetByEmailIncludRoadtrips(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<User>.Success(user.Value));

        // Act
        var result = await currentUserService.GetCurrentUser(CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(user.Value, result.Value);

    }

    [TestMethod]
    public async Task GetCurrentUser_ReturnsFailure_WhenEmailClaimIsNotFound()
    {
        // Arrange
        _mockHttpContextAccessor
            .Setup(x => x.HttpContext!.User.Claims)
            .Returns([]);

        // Act
        var result = await currentUserService.GetCurrentUser(CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
        Assert.AreEqual(CurrentUserErrors.ClaimTypeNullError, result.Error);
    }

    [TestMethod]
    public async Task GetCurrentUser_ReturnsFailure_WhenUserNotFound()
    {
        // Arrange
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, "test@example.com")
        };
        _mockHttpContextAccessor
            .Setup(x => x.HttpContext!.User.Claims)
            .Returns(claims);
        _mockUserRepository
            .Setup(x => x.GetByEmailIncludRoadtrips(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<User>.Failure(UserErrors.UserNotFoundByEmailError));

        // Act
        var result = await currentUserService.GetCurrentUser(CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
        Assert.AreEqual(UserErrors.UserNotFoundByEmailError, result.Error);
    }
}
