﻿using EpicRoadTrip.Api.Controllers;
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
            .ReturnsAsync(Result<string>.Success(It.IsAny<string>()));

        var controller = new AuthenticationController(mockSender.Object);

        // Act
        var result = await controller.Register(It.IsAny<RegisterRequest>(), CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        Assert.AreEqual(It.IsAny<string>(), ((OkObjectResult)result.Result).Value);
    }

    [TestMethod]
    public async Task Register_ReturnsBadRequestResult_WhenRegisterCommandFails()
    {
        // Arrange
        var mockSender = new Mock<ISender>();
        mockSender.Setup(sender => sender.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<string>.Failure(It.IsAny<Error>()));
        
        var controller = new AuthenticationController(mockSender.Object);

        // Act
        var result = await controller.Register(It.IsAny<RegisterRequest>(), CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        Assert.AreEqual(It.IsAny<string>(), ((BadRequestObjectResult)result.Result).Value);
    }
}