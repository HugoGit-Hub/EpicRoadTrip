using EpicRoadTrip.Application.Routes.CreateRoute;
using EpicRoadTrip.Application.Routes.DeleteRoute;
using EpicRoadTrip.Application.Routes.GetAllRoute;
using EpicRoadTrip.Application.Routes.GetRoute;
using EpicRoadTrip.Application.Routes.GetRouteBetweenPoints;
using EpicRoadTrip.Application.Routes.UpdateRoute;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EpicRoadTrip.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RouteController(ISender sender) : Controller
{
    [Authorize]
    [HttpPost(nameof(Create))]
    public async Task<ActionResult<CreateRouteResponse>> Create(CreateRouteRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new CreateRouteCommand(request), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpGet(nameof(Get))]
    public async Task<ActionResult<GetRouteResponse>> Get(int id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetRouteQuery(id), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpGet(nameof(GetAll))]
    public async Task<ActionResult<IEnumerable<GetAllRouteResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllRouteQuery(), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpPost(nameof(GetRouteBetweenPoints))]
    public async Task<ActionResult<IEnumerable<GetRouteResponse>>> GetRouteBetweenPoints(GetRouteRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetRouteBetweenPointsQuery(request), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpPut(nameof(Update))]
    public async Task<ActionResult<UpdateRouteResponse>> Update(UpdateRouteRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new UpdateRouteCommand(request), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpDelete(nameof(Delete))]
    public async Task<ActionResult<DeleteRouteResponse>> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteRouteCommand(id), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}