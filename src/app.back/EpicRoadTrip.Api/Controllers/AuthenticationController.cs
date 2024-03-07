using EpicRoadTrip.Application.Authentication.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EpicRoadTrip.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController(ISender sender) : Controller
{
    [AllowAnonymous]
    [HttpPost(nameof(Register))]
    public async Task<ActionResult<string>> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new RegisterCommand(request), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    } 
}