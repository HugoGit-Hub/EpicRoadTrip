using EpicRoadTrip.Api.Controllers;
using EpicRoadTrip.Application.Institutions.CreateInstitution;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Institutions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EpicRoadTrip.Test.Api;

[TestClass]
public class InstitutionControllerTest
{
    [TestMethod]
    public async Task Create_ShouldReturnBadRequest_WhenCommandHandlerReturnsFailure()
    {
        // Arrange
        var mockSender = new Mock<ISender>();
        var request = new
        {
            Name = "Institution Name",
            Price = 100.0,
            PhoneNumber = "123456789",
            Email = "test@gmail.com",
            Address = "Institution Address",
            Type = InstitutionType.Hotel,
            CityId = 1
        }.Adapt<CreateInstitutionRequest>();
        mockSender
            .Setup(s => s.Send(It.IsAny<CreateInstitutionCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<CreateInstitutionResponse>.Failure(It.IsAny<Error>()));

        var controller = new InstitutionController(mockSender.Object);

        // Act
        var result = await controller.Create(request, CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task Create_ShouldReturnOk_WhenCommandHandlerReturnsSuccess()
    {
        // Arrange
        var mockSender = new Mock<ISender>();
        var request = new
        {
            Name = "Institution Name",
            Price = 100.0,
            PhoneNumber = "123456789",
            Email = "test@gmail.com",
            Address = "Institution Address",
            Type = InstitutionType.Hotel,
            CityId = 1
        }.Adapt<CreateInstitutionRequest>();
        var response = new
        {
            Id = 1,
            Name = "Institution Name",
            Price = 100.0,
            PhoneNumber = "123456789",
            Email = "test@gmail.com",
            Address = "Institution Address",
            Type = InstitutionType.Hotel,
            CityId = 1
        }.Adapt<CreateInstitutionResponse>();
        mockSender
            .Setup(s => s.Send(It.IsAny<CreateInstitutionCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<CreateInstitutionResponse>.Success(response));

        var controller = new InstitutionController(mockSender.Object);

        // Act
        var result = await controller.Create(request, CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
    }
}