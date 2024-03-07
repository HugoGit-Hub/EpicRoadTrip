using EpicRoadTrip.Application.Authentication.Register;
using EpicRoadTrip.Domain.Authentications;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Users;
using Moq;

namespace EpicRoadTrip.Test.Application.Register;

[TestClass]
public class RegisterCommandHandlerTest
{
    private const string Email = "example@gmail.com";
    private const string Password = "Password123";
    private const string Firstname = "Bar";
    private const string Lastname = "Foo";
    private const int Age = 25;
    private const bool Gender = true;

    [TestMethod]
    public async Task Handle_ReturnsSuccessResult_WhenAllServicesSucceed()
    {
        // Arrange
        var mockAuthenticationService = new Mock<IAuthenticationService>();
        var mockUserService = new Mock<IUserService>();
        var request = new RegisterRequest
        {
            Email = Email,
            Password = Password,
            FirstName = Firstname,
            LastName = Lastname,
            Age = Age,
            Gender = Gender
        };

        var command = new RegisterCommand(request);
        mockAuthenticationService
            .Setup(service => service.IsEmailAlreadyUse(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        
        mockAuthenticationService
            .Setup(service => service.Encrypt(It.IsAny<string>()))
            .Returns(Result<string>.Success("encryptedPassword"));
        
        mockAuthenticationService
            .Setup(service => service.HashWithSalt(It.IsAny<string>()))
            .Returns(Result<string>.Success("hashedAndSaltedPassword"));

        mockUserService
            .Setup(service => service.Create(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<User>.Success(It.IsAny<User>()));

        mockAuthenticationService
            .Setup(service => service.GenerateToken(It.IsAny<User>()))
            .Returns(Result<string>.Success("token"));
        
        var handler = new RegisterCommandHandler(mockAuthenticationService.Object, mockUserService.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
    }

    [TestMethod]
    public async Task Handle_ReturnsFailureResult_WhenIsEmailUniqueFails()
    {
        // Arrange
        var mockAuthenticationService = new Mock<IAuthenticationService>();
        var mockUserService = new Mock<IUserService>();
        var request = new RegisterRequest
        {
            Email = Email,
            Password = Password,
            FirstName = Firstname,
            LastName = Lastname,
            Age = Age,
            Gender = Gender
        };

        var command = new RegisterCommand(request);
        mockAuthenticationService
            .Setup(service => service.IsEmailAlreadyUse(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        
        var handler = new RegisterCommandHandler(mockAuthenticationService.Object, mockUserService.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }

    [TestMethod]
    public async Task Handle_ReturnsFailureResult_WhenEncryptFails()
    {
        // Arrange
        var mockAuthenticationService = new Mock<IAuthenticationService>();
        var mockUserService = new Mock<IUserService>();
        var request = new RegisterRequest
        {
            Email = Email,
            Password = Password,
            FirstName = Firstname,
            LastName = Lastname,
            Age = Age,
            Gender = Gender
        };

        var command = new RegisterCommand(request);
        mockAuthenticationService
            .Setup(service => service.IsEmailAlreadyUse(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        mockAuthenticationService
            .Setup(service => service.Encrypt(It.IsAny<string>()))
            .Returns(Result<string>.Failure(It.IsAny<Error>()));

        var handler = new RegisterCommandHandler(mockAuthenticationService.Object, mockUserService.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }

    [TestMethod]
    public async Task Handle_ReturnsFailureResult_WhenHashWithSaltFails()
    {
        // Arrange
        var mockAuthenticationService = new Mock<IAuthenticationService>();
        var mockUserService = new Mock<IUserService>();
        var request = new RegisterRequest
        {
            Email = Email,
            Password = Password,
            FirstName = Firstname,
            LastName = Lastname,
            Age = Age,
            Gender = Gender
        };

        var command = new RegisterCommand(request);
        mockAuthenticationService
            .Setup(service => service.IsEmailAlreadyUse(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        mockAuthenticationService
            .Setup(service => service.Encrypt(It.IsAny<string>()))
            .Returns(Result<string>.Success("encryptedPassword"));

        mockAuthenticationService
            .Setup(service => service.HashWithSalt(It.IsAny<string>()))
            .Returns(Result<string>.Failure(It.IsAny<Error>()));

        var handler = new RegisterCommandHandler(mockAuthenticationService.Object, mockUserService.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }

    [TestMethod]
    public async Task Handle_ReturnsFailureResult_WhenCreateUserFails()
    {
        // Arrange
        var mockAuthenticationService = new Mock<IAuthenticationService>();
        var mockUserService = new Mock<IUserService>();
        var request = new RegisterRequest
        {
            Email = Email,
            Password = Password,
            FirstName = Firstname,
            LastName = Lastname,
            Age = -1,
            Gender = Gender
        };

        var command = new RegisterCommand(request);
        mockAuthenticationService
            .Setup(service => service.IsEmailAlreadyUse(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        mockAuthenticationService
            .Setup(service => service.Encrypt(It.IsAny<string>()))
            .Returns(Result<string>.Success("encryptedPassword"));

        mockAuthenticationService
            .Setup(service => service.HashWithSalt(It.IsAny<string>()))
            .Returns(Result<string>.Success("hashedAndSaltedPassword"));

        var handler = new RegisterCommandHandler(mockAuthenticationService.Object, mockUserService.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }

    [TestMethod]
    public async Task Handle_ReturnsFailureResult_WhenCreateUserServiceFails()
    {
        // Arrange
        var mockAuthenticationService = new Mock<IAuthenticationService>();
        var mockUserService = new Mock<IUserService>();
        var request = new RegisterRequest
        {
            Email = Email,
            Password = Password,
            FirstName = Firstname,
            LastName = Lastname,
            Age = Age,
            Gender = Gender
        };

        var command = new RegisterCommand(request);
        mockAuthenticationService
            .Setup(service => service.IsEmailAlreadyUse(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        mockAuthenticationService
            .Setup(service => service.Encrypt(It.IsAny<string>()))
            .Returns(Result<string>.Success("encryptedPassword"));

        mockAuthenticationService
            .Setup(service => service.HashWithSalt(It.IsAny<string>()))
            .Returns(Result<string>.Success("hashedAndSaltedPassword"));

        mockUserService
            .Setup(service => service.Create(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<User>.Failure(It.IsAny<Error>()));

        var handler = new RegisterCommandHandler(mockAuthenticationService.Object, mockUserService.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }
    
    [TestMethod]
    public async Task Handle_ReturnsFailureResult_WhenGenerateTokenFails()
    {
        // Arrange
        var mockAuthenticationService = new Mock<IAuthenticationService>();
        var mockUserService = new Mock<IUserService>();
        var request = new RegisterRequest
        {
            Email = Email,
            Password = Password,
            FirstName = Firstname,
            LastName = Lastname,
            Age = Age,
            Gender = Gender
        };

        var command = new RegisterCommand(request);
        mockAuthenticationService
            .Setup(service => service.IsEmailAlreadyUse(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        mockAuthenticationService
            .Setup(service => service.Encrypt(It.IsAny<string>()))
            .Returns(Result<string>.Success("encryptedPassword"));

        mockAuthenticationService
            .Setup(service => service.HashWithSalt(It.IsAny<string>()))
            .Returns(Result<string>.Success("hashedAndSaltedPassword"));

        mockUserService
            .Setup(service => service.Create(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<User>.Success(It.IsAny<User>()));

        mockAuthenticationService
            .Setup(service => service.GenerateToken(It.IsAny<User>()))
            .Returns(Result<string>.Failure(It.IsAny<Error>()));

        var handler = new RegisterCommandHandler(mockAuthenticationService.Object, mockUserService.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailure);
    }
}
