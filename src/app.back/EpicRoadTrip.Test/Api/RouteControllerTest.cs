using EpicRoadTrip.Api.Controllers;
using EpicRoadTrip.Application.Routes.CreateRoute;
using EpicRoadTrip.Application.Routes.DeleteRoute;
using EpicRoadTrip.Application.Routes.GetAllRoute;
using EpicRoadTrip.Application.Routes.GetRoute;
using EpicRoadTrip.Application.Routes.UpdateRoute;
using EpicRoadTrip.Domain.ErrorHandling;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EpicRoadTrip.Test.Api;

[TestClass]
public class RouteControllerTest
{
    [TestMethod]
    public async Task Create_ShouldReturnBadRequest_WhenCommandHandlerReturnsFailure()
    {
        // Arrange
        var mockSender = new Mock<ISender>();
        var request = new
        {
            Distance = 100.0,
            Duration = 100,
            CityOneId = 1,
            CityTwoId = 2,
            RoadtripId = 1
        }.Adapt<CreateRouteRequest>();
        mockSender
            .Setup(s => s.Send(It.IsAny<CreateRouteCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<CreateRouteResponse>.Failure(It.IsAny<Error>()));

        var controller = new RouteController(mockSender.Object);

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
            Distance = 100.0,
            Duration = 100,
            CityOneId = 1,
            CityTwoId = 2,
            RoadtripId = 1
        }.Adapt<CreateRouteRequest>();
        var response = new
        {
            Id = 1,
            Distance = 100.0,
            Duration = TimeSpan.FromMinutes(100),
            CityOneId = 1,
            CityTwoId = 2,
            RoadtripId = 1
        }.Adapt<CreateRouteResponse>();
        mockSender
            .Setup(s => s.Send(It.IsAny<CreateRouteCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<CreateRouteResponse>.Success(response));

        var controller = new RouteController(mockSender.Object);

        // Act
        var result = await controller.Create(request, CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
    }

    [TestMethod]
    public async Task Get_ShouldReturnBadRequest_WhenQueryHandlerReturnsFailure()
    {
        // Arrange
        var mockSender = new Mock<ISender>();
        const int id = 1;
        mockSender.Setup(s => s.Send(It.IsAny<GetRouteQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<GetRouteResponse>.Failure(It.IsAny<Error>()));

        var controller = new RouteController(mockSender.Object);

        // Act
        var result = await controller.Get(id, CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task Get_ShouldReturnOk_WhenQueryHandlerReturnsSuccess()
    {
        // Arrange
        var mockSender = new Mock<ISender>();
        const int id = 1;
        var response = new GetRouteResponse();
        mockSender.Setup(s => s.Send(It.IsAny<GetRouteQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<GetRouteResponse>.Success(response));

        var controller = new RouteController(mockSender.Object);

        // Act
        var result = await controller.Get(id, CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
    }

    [TestMethod]
    public async Task GetAll_ShouldReturnBadRequest_WhenQueryHandlerReturnsFailure()
    {
        // Arrange
        var mockSender = new Mock<ISender>();
        mockSender.Setup(s => s.Send(It.IsAny<GetAllRouteQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<IEnumerable<GetAllRouteResponse>>.Failure(It.IsAny<Error>()));

        var controller = new RouteController(mockSender.Object);

        // Act
        var result = await controller.GetAll(CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task GetAll_ShouldReturnOk_WhenQueryHandlerReturnsSuccess()
    {
        // Arrange
        var mockSender = new Mock<ISender>();
        var response = new List<GetAllRouteResponse>();
        mockSender.Setup(s => s.Send(It.IsAny<GetAllRouteQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<IEnumerable<GetAllRouteResponse>>.Success(response));

        var controller = new RouteController(mockSender.Object);

        // Act
        var result = await controller.GetAll(CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
    }

    [TestMethod]
    public async Task Update_ShouldReturnBadRequest_WhenCommandHandlerReturnsFailure()
    {
        // Arrange
        var mockSender = new Mock<ISender>();
        var request = new UpdateRouteRequest { /* populate properties here */ };
        mockSender.Setup(s => s.Send(It.IsAny<UpdateRouteCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<UpdateRouteResponse>.Failure(It.IsAny<Error>()));

        var controller = new RouteController(mockSender.Object);

        // Act
        var result = await controller.Update(request, CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task Update_ShouldReturnOk_WhenCommandHandlerReturnsSuccess()
    {
        // Arrange
        var mockSender = new Mock<ISender>();
        var request = new UpdateRouteRequest { /* populate properties here */ };
        var response = new UpdateRouteResponse();
        mockSender.Setup(s => s.Send(It.IsAny<UpdateRouteCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<UpdateRouteResponse>.Success(response));

        var controller = new RouteController(mockSender.Object);

        // Act
        var result = await controller.Update(request, CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
    }

    [TestMethod]
    public async Task Delete_ShouldReturnBadRequest_WhenCommandHandlerReturnsFailure()
    {
        // Arrange
        var mockSender = new Mock<ISender>();
        var id = 1;
        mockSender.Setup(s => s.Send(It.IsAny<DeleteRouteCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<DeleteRouteResponse>.Failure(It.IsAny<Error>()));

        var controller = new RouteController(mockSender.Object);

        // Act
        var result = await controller.Delete(id, CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task Delete_ShouldReturnOk_WhenCommandHandlerReturnsSuccess()
    {
        // Arrange
        var mockSender = new Mock<ISender>();
        var id = 1;
        var response = new DeleteRouteResponse();
        mockSender.Setup(s => s.Send(It.IsAny<DeleteRouteCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<DeleteRouteResponse>.Success(response));

        var controller = new RouteController(mockSender.Object);

        // Act
        var result = await controller.Delete(id, CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
    }

}