using EpicRoadTrip.Application.Authentications.Logins;
using EpicRoadTrip.Domain.Authentications;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Users;
using Moq;

namespace EpicRoadTrip.Test.Application.Authentications.Logins;

[TestClass]
public class LoginCommandHandlerTest()
{
    [TestMethod]
    public async Task Handle_ShouldReturnSuccess_WhenCredentialsAreCorrect()
    {
        // Arrange
        var authenticationService = new Mock<IAuthenticationService>();
        var userService = new Mock<IUserService>();
        var request = new LoginRequest
        {
            Email = "test@gmail.com",
            Password = "password"
        };
        var user = User.Create(
            "Test",
            "Test",
            "test@gmail.com",
            "password",
            20,
            true);

        var handler = new LoginCommandHandler(authenticationService.Object, userService.Object);
        authenticationService
            .Setup(x => x.Encrypt(It.IsAny<string>()))
            .Returns(Result<string>.Success("encryptedPassword"));

        authenticationService
            .Setup(x => x.HashWithSalt(It.IsAny<string>()))
            .Returns(Result<string>.Success("hashedAndSaltedPassword"));

        authenticationService
            .Setup(x => x.AreCredentialscorrects(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(true));

        userService
            .Setup(x => x.GetByEmail(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result<User>.Success(user.Value)));

        authenticationService
            .Setup(x => x.GenerateToken(It.IsAny<User>()))
            .Returns(Result<string>.Success("token"));

        // Act
        var result = await handler.Handle(new LoginCommand(request), CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
    }

    [TestMethod]
    public async Task Handle_ShouldReturnFailure_WhenGenerateTokenFail()
    {
        // Arrange
        var authenticationService = new Mock<IAuthenticationService>();
        var userService = new Mock<IUserService>();
        var request = new LoginRequest
        {
            Email = "test@gmail.com",
            Password = "password"
        };
        var user = User.Create(
            "Test",
            "Test",
            "test@gmail.com",
            "password",
            20,
            true);

        var handler = new LoginCommandHandler(authenticationService.Object, userService.Object);
        authenticationService
            .Setup(x => x.Encrypt(It.IsAny<string>()))
            .Returns(Result<string>.Success("encryptedPassword"));

        authenticationService
            .Setup(x => x.HashWithSalt(It.IsAny<string>()))
            .Returns(Result<string>.Success("hashedAndSaltedPassword"));

        authenticationService
            .Setup(x => x.AreCredentialscorrects(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(true));

        userService
            .Setup(x => x.GetByEmail(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result<User>.Success(user.Value)));

        authenticationService
            .Setup(x => x.GenerateToken(It.IsAny<User>()))
            .Returns(Result<string>.Failure(It.IsAny<Error>()));

        // Act
        var result = await handler.Handle(new LoginCommand(request), CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }

    [TestMethod]
    public async Task Handle_ShouldReturnFailure_WhenGetByEmailFail()
    {
        // Arrange
        var authenticationService = new Mock<IAuthenticationService>();
        var userService = new Mock<IUserService>();
        var request = new LoginRequest
        {
            Email = "test@gmail.com",
            Password = "password"
        };

        var handler = new LoginCommandHandler(authenticationService.Object, userService.Object);
        authenticationService
            .Setup(x => x.Encrypt(It.IsAny<string>()))
            .Returns(Result<string>.Success("encryptedPassword"));

        authenticationService
            .Setup(x => x.HashWithSalt(It.IsAny<string>()))
            .Returns(Result<string>.Success("hashedAndSaltedPassword"));

        authenticationService
            .Setup(x => x.AreCredentialscorrects(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(true));

        userService
            .Setup(x => x.GetByEmail(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result<User>.Failure(It.IsAny<Error>())));

        // Act
        var result = await handler.Handle(new LoginCommand(request), CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }

    [TestMethod]
    public async Task Handle_ShouldReturnFailure_WhenAreCredentialscorrectsFail()
    {
        // Arrange
        var authenticationService = new Mock<IAuthenticationService>();
        var userService = new Mock<IUserService>();
        var request = new LoginRequest
        {
            Email = "test@gmail.com",
            Password = "password"
        };

        var handler = new LoginCommandHandler(authenticationService.Object, userService.Object);
        authenticationService
            .Setup(x => x.Encrypt(It.IsAny<string>()))
            .Returns(Result<string>.Success("encryptedPassword"));

        authenticationService
            .Setup(x => x.HashWithSalt(It.IsAny<string>()))
            .Returns(Result<string>.Success("hashedAndSaltedPassword"));

        authenticationService
            .Setup(x => x.AreCredentialscorrects(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(false));

        // Act
        var result = await handler.Handle(new LoginCommand(request), CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }

    [TestMethod]
    public async Task Handle_ShouldReturnFailure_WhenHashWithSaltFail()
    {
        // Arrange
        var authenticationService = new Mock<IAuthenticationService>();
        var userService = new Mock<IUserService>();
        var request = new LoginRequest
        {
            Email = "test@gmail.com",
            Password = "password"
        };

        var handler = new LoginCommandHandler(authenticationService.Object, userService.Object);
        authenticationService
            .Setup(x => x.Encrypt(It.IsAny<string>()))
            .Returns(Result<string>.Success("encryptedPassword"));

        authenticationService
            .Setup(x => x.HashWithSalt(It.IsAny<string>()))
            .Returns(Result<string>.Failure(It.IsAny<Error>()));

        // Act
        var result = await handler.Handle(new LoginCommand(request), CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }

    [TestMethod]
    public async Task Handle_ShouldReturnFailure_WhenEncryptFail()
    {
        // Arrange
        var authenticationService = new Mock<IAuthenticationService>();
        var userService = new Mock<IUserService>();
        var request = new LoginRequest
        {
            Email = "test@gmail.com",
            Password = "password"
        };

        var handler = new LoginCommandHandler(authenticationService.Object, userService.Object);
        authenticationService
            .Setup(x => x.Encrypt(It.IsAny<string>()))
            .Returns(Result<string>.Failure(It.IsAny<Error>()));

        // Act
        var result = await handler.Handle(new LoginCommand(request), CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }
}