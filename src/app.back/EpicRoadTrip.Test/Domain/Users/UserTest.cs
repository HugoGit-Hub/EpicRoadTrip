using EpicRoadTrip.Domain.Users;
using Moq;

namespace EpicRoadTrip.Test.Domain.Users;

[TestClass]
public class UserTest
{
    private const string FirstName = "John";
    private const string LastName = "Doe";
    private const string Email = "john.doe@example.com";
    private const string Password = "password123";
    private const int Age = 25;
    private const bool Gender = true;

    [TestMethod]
    public void CreateUser_ValidParameters_CreatesUser()
    {
        // Act
        var user = User.Create(FirstName, LastName, Email, Password, Age, Gender);

        // Assert
        Assert.AreEqual(FirstName, user.Value.FirstName);
        Assert.AreEqual(LastName, user.Value.LastName);
        Assert.AreEqual(Email, user.Value.Email);
        Assert.AreEqual(Password, user.Value.Password);
        Assert.AreEqual(Age, user.Value.Age);
        Assert.AreEqual(Gender, user.Value.Gender);
    }

    [TestMethod]
    public void CreateUser_InvalidParameters_ThrowsException()
    {
        // Arrange
        var firstName = It.IsAny<string>();
        var lastName = It.IsAny<string>();
        var email = It.IsAny<string>();
        var password = It.IsAny<string>();

        // Act
        var user = User.Create(firstName, lastName, email, password, Age, Gender);

        // Assert
        Assert.IsTrue(user.IsFailure);
    }

    [TestMethod]
    public void CreateUser_InvalidAge_ThrowsException()
    {
        // Arrange
        const int age = -10;

        // Act
        var user = User.Create(FirstName, LastName, Email, Password, age, Gender);

        // Assert
        Assert.IsTrue(user.IsFailure);
    }
}