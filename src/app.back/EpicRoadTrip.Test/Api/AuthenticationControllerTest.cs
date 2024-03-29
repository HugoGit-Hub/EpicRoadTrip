﻿using EpicRoadTrip.Api.Controllers;
using EpicRoadTrip.Application.Authentications;
using EpicRoadTrip.Application.Authentications.Logins;
using EpicRoadTrip.Application.Authentications.Registers;
using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EpicRoadTrip.Test.Api;

[TestClass]
public class AuthenticationControllerTest
{
    [TestMethod]
    public async Task Register_ReturnsOkResult_WhenRegisterCommandSucceeds()
    {
        // Arrange
        var mockSender = new Mock<ISender>();
        mockSender
            .Setup(sender => sender.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<RegisterResponse>.Success(It.IsAny<RegisterResponse>()));

        var controller = new AuthenticationController(mockSender.Object);

        // Act
        var result = await controller.Register(It.IsAny<RegisterRequest>(), CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        Assert.AreEqual(It.IsAny<RegisterResponse>(), ((OkObjectResult)result.Result).Value);
    }

    [TestMethod]
    public async Task Register_ReturnsBadRequestResult_WhenRegisterCommandFails()
    {
        // Arrange
        var mockSender = new Mock<ISender>();
        mockSender.Setup(sender => sender.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<RegisterResponse>.Failure(It.IsAny<Error>()));
        
        var controller = new AuthenticationController(mockSender.Object);

        // Act
        var result = await controller.Register(It.IsAny<RegisterRequest>(), CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        Assert.AreEqual(It.IsAny<RegisterResponse>(), ((BadRequestObjectResult)result.Result).Value);
    }

    [TestMethod]
    public async Task Login_ReturnsOkResult_WhenLoginCommandSucceeds()
    {
        // Arrange
        var mockSender = new Mock<ISender>();
        mockSender
            .Setup(sender => sender.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<LoginResponse>.Success(It.IsAny<LoginResponse>()));

        var controller = new AuthenticationController(mockSender.Object);

        // Act
        var result = await controller.Login(It.IsAny<LoginRequest>(), CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        Assert.AreEqual(It.IsAny<LoginResponse>(), ((OkObjectResult)result.Result).Value);
    }

    [TestMethod]
    public async Task Login_ReturnsUnauthorizedResult_WhenLoginCommandFailsWithInvalidEmailOrPasswordError()
    {
        // Arrange
        var mockSender = new Mock<ISender>();
        mockSender
            .Setup(sender => sender.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<LoginResponse>.Failure(AuthenticationErrors.InvalidEmailOrPasswordError));

        var controller = new AuthenticationController(mockSender.Object);

        // Act
        var result = await controller.Login(It.IsAny<LoginRequest>(), CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(UnauthorizedObjectResult));
        Assert.AreEqual(AuthenticationErrors.InvalidEmailOrPasswordError, ((UnauthorizedObjectResult)result.Result).Value);
    }

    [TestMethod]
    public async Task Login_ReturnsBadRequestResult_WhenLoginCommandFails()
    {
        // Arrange
        var mockSender = new Mock<ISender>();
        mockSender
            .Setup(sender => sender.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<LoginResponse>.Failure(It.IsAny<Error>()));

        var controller = new AuthenticationController(mockSender.Object);

        // Act
        var result = await controller.Login(It.IsAny<LoginRequest>(), CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        Assert.AreEqual(It.IsAny<LoginResponse>(), ((BadRequestObjectResult)result.Result).Value);
    }
}