using EpicRoadTrip.Application.Roadtrips.CreateRoadtrip;
using EpicRoadTrip.Application.Roadtrips.DeleteRoadtrip;
using EpicRoadTrip.Application.Roadtrips.GetAllRoadtrip;
using EpicRoadTrip.Application.Roadtrips.GetRoadtrip;
using EpicRoadTrip.Application.Roadtrips.UpdateRoadtrip;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EpicRoadTrip.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoadtripController(ISender sender) : Controller
{
    [Authorize]
    [HttpPost(nameof(Create))]
    public async Task<ActionResult<CreateRoadtripResponse>> Create(CreateRoadtripRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new CreateRoadtripCommand(request), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpGet(nameof(Get))]
    public async Task<ActionResult<GetRoadtripResponse>> Get(int id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetRoadtripQuery(id), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpGet(nameof(GetAll))]
    public async Task<ActionResult<IEnumerable<GetRoadtripResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllRoadtripQuery(), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpPut(nameof(Update))]
    public async Task<ActionResult<UpdateRoadtripResponse>> Update(UpdateRoadtripRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new UpdateRoadtripCommand(request), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpDelete(nameof(Delete))]
    public async Task<ActionResult<DeleteRoadtripResponse>> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteRoadtripCommand(id), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}