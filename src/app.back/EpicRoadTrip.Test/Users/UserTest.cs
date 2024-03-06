using EpicRoadTrip.Domain.Users;
using EpicRoadTrip.Domain.Users.Exceptions;
using Moq;

namespace EpicRoadTrip.Test.Users;

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
        Assert.AreEqual(FirstName, user.FirstName);
        Assert.AreEqual(LastName, user.LastName);
        Assert.AreEqual(Email, user.Email);
        Assert.AreEqual(Password, user.Password);
    }

    [TestMethod]
    [ExpectedException(typeof(UserInvalidFormatException))]
    public void CreateUser_InvalidParameters_ThrowsException()
    {
        // Arrange
        var firstName = It.IsAny<string>();
        var lastName = It.IsAny<string>();
        var email = It.IsAny<string>();
        var password = It.IsAny<string>();

        // Act
        User.Create(firstName, lastName, email, password, Age, Gender);
    }

    [TestMethod]
    [ExpectedException(typeof(UserInvalidAgeException))]
    public void CreateUser_InvalidAge_ThrowsException()
    {
        // Arrange
        const int age = -10;

        // Act
        User.Create(FirstName, LastName, Email, Password, age, Gender);
    }
}