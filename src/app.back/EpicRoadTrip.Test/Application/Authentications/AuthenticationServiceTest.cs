using EpicRoadTrip.Application.Authentications;
using EpicRoadTrip.Application.Options;
using EpicRoadTrip.Domain.Users;
using Microsoft.Extensions.Configuration;
using Moq;

namespace EpicRoadTrip.Test.Application.Authentications;

[TestClass]
public class AuthenticationServiceTest(AuthenticationService authenticationService)
{
    private readonly Mock<IConfiguration> _configurationMock = new();
    private readonly Mock<IAuthenticationRepository> _authenticationRepositoryMock = new();

    [TestInitialize]
    public void Setup()
    {
        authenticationService = new AuthenticationService(_configurationMock.Object, _authenticationRepositoryMock.Object);
    }

    [TestMethod]
    public async Task IsEmailAlreadyUse_ShouldReturnTrue()
    {
        // Arrange
        const string email = "test@example.com";

        // Act
        var result = await authenticationService.IsEmailAlreadyUse(email, CancellationToken.None);
        
        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task AreCredentialscorrects_ShouldReturnTrue()
    {
        // Arrange
        const string email = "test@gmail.com";
        const string password = "password";

        // Act
        var result = await authenticationService.AreCredentialscorrects(email, password, CancellationToken.None);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Encrypt_ShouldReturnResultSuccess()
    {
        // Arrange
        const string content = "password";
        const string encodingKey = "0123456789ABCDEF";
        _configurationMock
            .Setup(x => x.GetSection(nameof(EncodingKey))[nameof(EncodingKey.Value)])
            .Returns(encodingKey);

        // Act
        var result = authenticationService.Encrypt(content);

        // Assert
        Assert.IsTrue(result.IsSuccess);
    }

    [TestMethod]
    public void Encrypt_ShouldReturnResultFailure_WithEmptyEncodingKey()
    {
        // Arrange
        const string content = "password";
        const string encodingKey = "";
        _configurationMock
            .Setup(x => x.GetSection(nameof(EncodingKey))[nameof(EncodingKey.Value)])
            .Returns(encodingKey);

        // Act
        var result = authenticationService.Encrypt(content);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public void Encrypt_SHouldReturnResultFailure_WithInvalidEncodingKeyLength()
    {
        // Arrange
        const string content = "password";
        const string encodingKey = "0123456789";
        _configurationMock
            .Setup(x => x.GetSection(nameof(EncodingKey))[nameof(EncodingKey.Value)])
            .Returns(encodingKey);

        // Act
        var result = authenticationService.Encrypt(content);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public void HashWithSalt_ShouldReturnResultSuccess()
    {
        // Arrange
        const string content = "password";
        const string salt = "0123456789ABCDEF";
        _configurationMock
            .Setup(x => x.GetSection(nameof(Salt))[nameof(Salt.Value)])
            .Returns(salt);

        // Act
        var result = authenticationService.HashWithSalt(content);

        // Assert
        Assert.IsTrue(result.IsSuccess);
    }

    [TestMethod]
    public void HashWithSalt_ShouldReturnResultFailure_WithEmptySalt()
    {
        // Arrange
        const string content = "password";
        const string salt = "";
        _configurationMock
            .Setup(x => x.GetSection(nameof(Salt))[nameof(Salt.Value)])
            .Returns(salt);

        // Act
        var result = authenticationService.HashWithSalt(content);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public void GenerateToken_ShouldReturnResultSuccess()
    {
        // Arrange
        const string issuer = "issuer";
        const string audience = "audience";
        const string key = "4F8A760BCD2E09A1E3F514C68E4F37D9";
        _configurationMock
            .Setup(x => x.GetSection(nameof(Jwt))[nameof(Jwt.Issuer)])
            .Returns(issuer);
        _configurationMock
            .Setup(x => x.GetSection(nameof(Jwt))[nameof(Jwt.Audience)])
            .Returns(audience);
        _configurationMock
            .Setup(x => x.GetSection(nameof(Jwt))[nameof(Jwt.Key)])
            .Returns(key);

        const string firstName = "John";
        const string lastName = "Doe";
        const string email = "test@gmail.com";
        const string password = "password";
        const int age = 25;
        const bool gender = true;
        var user = User.Create(1, firstName, lastName, email, password, age, gender);

        // Act
        var result = authenticationService.GenerateToken(user.Value);

        // Assert
        Assert.IsTrue(result.IsSuccess);
    }

    [TestMethod]
    public void GenerateToken_ShouldReturnResultFailure_WithEmptyIssuerOrAudianceOrKey()
    {
        // Arrange
        const string issuer = "";
        const string audience = "";
        const string key = "";
        _configurationMock
            .Setup(x => x.GetSection(nameof(Jwt))[nameof(Jwt.Issuer)])
            .Returns(issuer);
        _configurationMock
            .Setup(x => x.GetSection(nameof(Jwt))[nameof(Jwt.Audience)])
            .Returns(audience);
        _configurationMock
            .Setup(x => x.GetSection(nameof(Jwt))[nameof(Jwt.Key)])
            .Returns(key);

        const string firstName = "John";
        const string lastName = "Doe";
        const string email = "test@gmail.com";
        const string password = "password";
        const int age = 25;
        const bool gender = true;
        var user = User.Create(1, firstName, lastName, email, password, age, gender);

        // Act
        var result = authenticationService.GenerateToken(user.Value);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public void GenerateToken_ShouldReturnResultFailure_WithWrongKey()
    {
        // Arrange
        const string issuer = "issuer";
        const string audience = "audience";
        const string key = "test";
        _configurationMock
            .Setup(x => x.GetSection(nameof(Jwt))[nameof(Jwt.Issuer)])
            .Returns(issuer);
        _configurationMock
            .Setup(x => x.GetSection(nameof(Jwt))[nameof(Jwt.Audience)])
            .Returns(audience);
        _configurationMock
            .Setup(x => x.GetSection(nameof(Jwt))[nameof(Jwt.Key)])
            .Returns(key);

        const string firstName = "John";
        const string lastName = "Doe";
        const string email = "test@gmail.com";
        const string password = "password";
        const int age = 25;
        const bool gender = true;
        var user = User.Create(1, firstName, lastName, email, password, age, gender);

        // Act
        var result = authenticationService.GenerateToken(user.Value);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }
}