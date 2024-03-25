using EpicRoadTrip.Application.Institutions.CreateInstitution;
using EpicRoadTrip.Application.Institutions.DeleteInstitution;
using EpicRoadTrip.Application.Institutions.GetAllInstitution;
using EpicRoadTrip.Application.Institutions.GetInstitution;
using EpicRoadTrip.Application.Institutions.GetInstitutionAround;
using EpicRoadTrip.Application.Institutions.UpdateInstitution;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EpicRoadTrip.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstitutionController(ISender sender) : Controller
{
    [Authorize]
    [HttpPost(nameof(Create))]
    public async Task<ActionResult<CreateInstitutionResponse>> Create(CreateInstitutionRequest request, CancellationToken cancellationTokn)
    {
        var result = await sender.Send(new CreateInstitutionCommand(request), cancellationTokn);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpPost(nameof(GetInstitutionAround))]
    public async Task<ActionResult<GetInstitutionAroundResponse>> GetInstitutionAround(GetInstitutionAroundRequest request, CancellationToken cancellationTokn)
    {
        var result = await sender.Send(new GetInstitutionAroundQuery(request), cancellationTokn);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpGet(nameof(Get))]
    public async Task<ActionResult<GetInstitutionResponse>> Get(int id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetInstitutionQuery(id), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpGet(nameof(GetAll))]
    public async Task<ActionResult<IEnumerable<GetInstitutionResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllInstitutionQuery(), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpPut(nameof(Update))]
    public async Task<ActionResult<UpdateInstitutionResponse>> Update(UpdateInstitutionRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new UpdateInstitutionCommand(request), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpDelete(nameof(Delete))]
    public async Task<ActionResult<DeleteInstitutionResponse>> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteInstitutionCommand(id), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}