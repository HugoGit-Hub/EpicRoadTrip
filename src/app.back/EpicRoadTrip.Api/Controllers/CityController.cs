using EpicRoadTrip.Application.Cities.CreateCity;
using EpicRoadTrip.Application.Cities.DeleteCity;
using EpicRoadTrip.Application.Cities.GetAllCity;
using EpicRoadTrip.Application.Cities.GetCity;
using EpicRoadTrip.Application.Cities.UpdateCity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EpicRoadTrip.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CityController(ISender sender) : Controller
{
    [Authorize]
    [HttpPost(nameof(Create))]
    public async Task<ActionResult<CreateCityResponse>> Create(string name, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new CreateCityCommand(name), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpGet(nameof(Get))]
    public async Task<ActionResult<GetCityResponse>> Get(int id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetCityQuery(id), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpGet(nameof(GetAll))]
    public async Task<ActionResult<IEnumerable<GetCityResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllCityQuery(), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpPut(nameof(Update))]
    public async Task<ActionResult<UpdateCityResponse>> Update(UpdateCityRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new UpdateCityCommand(request), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpDelete(nameof(Delete))]
    public async Task<ActionResult<DeleteCityResponse>> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteCityCommand(id), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}