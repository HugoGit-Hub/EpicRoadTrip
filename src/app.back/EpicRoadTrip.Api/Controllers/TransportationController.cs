using EpicRoadTrip.Application.Transportations.CreateTransportation;
using EpicRoadTrip.Application.Transportations.DeleteTransportation;
using EpicRoadTrip.Application.Transportations.GetAllTransportation;
using EpicRoadTrip.Application.Transportations.GetTransportation;
using EpicRoadTrip.Application.Transportations.UpdateTransportation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EpicRoadTrip.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransportationController(ISender sender) : Controller
{
    [Authorize]
    [HttpPost(nameof(Create))]
    public async Task<ActionResult<CreateTransportationResponse>> Create(CreateTransportationRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new CreateTransportationCommand(request), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpGet(nameof(Get))]
    public async Task<ActionResult<GetTransportationResponse>> Get(int id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetTransportationQuery(id), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpGet(nameof(GetAll))]
    public async Task<ActionResult<IEnumerable<GetTransportationResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllTransportationQuery(), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpPut(nameof(Update))]
    public async Task<ActionResult<UpdateTransportationResponse>> Update(UpdateTransportationRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new UpdateTransportationCommand(request), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpDelete(nameof(Delete))]
    public async Task<ActionResult<Unit>> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteTransportationCommand(id), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}