using EpicRoadTrip.Domain.Users;
using EpicRoadTrip.Domain.Users.Exceptions;
using Moq;

namespace EpicRoadTrip.Test.Users;

[TestClass]
public class UserTest
{
    private const int Id = 1;

    [TestMethod]
    public void CreateUser_ValidParameters_CreatesUser()
    {
        // Arrange
        const string firstName = "John";
        const string lastName = "Doe";
        const string email = "john.doe@example.com";
        const string password = "password123";

        // Act
        var user = User.Create(Id, firstName, lastName, email, password);

        // Assert
        Assert.AreEqual(Id, user.Id);
        Assert.AreEqual(firstName, user.FirstName);
        Assert.AreEqual(lastName, user.LastName);
        Assert.AreEqual(email, user.Email);
        Assert.AreEqual(password, user.Password);
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
        User.Create(Id, firstName, lastName, email, password);
    }
}