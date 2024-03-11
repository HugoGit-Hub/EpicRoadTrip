using EpicRoadTrip.Application.Transports;
using EpicRoadTrip.Application.Users;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Users;
using Moq;

namespace EpicRoadTrip.Test.Application.Users;

[TestClass]
public class UserServiceTest
{
    private const string Firstname = "John";
    private const string Lastname = "Doe";
    private const string Email = "test@example.com";
    private const string Password = "password";
    private const int Age = 25;
    private const bool Gender = true;

    [TestMethod]
    public async Task GetByEmail_WhenUserExists_ShouldReturnSuccess()
    {
        // Arrange
        var userRepository = new Mock<IUserRepository>();
        var user = User.Create(1, Firstname, Lastname, Email, Password, Age, Gender);

        userRepository
            .Setup(x => x.GetByEmail(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<User>.Success(user.Value));

        var userService = new UserService(userRepository.Object);

        // Act
        var result = await userService.GetByEmail(It.IsAny<string>(), CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
    }

    [TestMethod]
    public async Task GetByEmail_WhenUserDoNotExists_ShouldReturnFailure()
    {
        // Arrange
        var userRepository = new Mock<IUserRepository>();
        var user = User.Create(1, Firstname, Lastname, Email, Password, Age, Gender);

        userRepository
            .Setup(x => x.GetByEmail(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<User>.Failure(It.IsAny<Error>()));

        var userService = new UserService(userRepository.Object);

        // Act
        var result = await userService.GetByEmail(It.IsAny<string>(), CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }
}