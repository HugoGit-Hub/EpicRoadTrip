using EpicRoadTrip.Application.Authentications;
using EpicRoadTrip.Application.Authentications.Logins;
using EpicRoadTrip.Application.Authentications.Registers;
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

    [AllowAnonymous]
    [HttpPost(nameof(Login))]
    public async Task<ActionResult<string>> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new LoginCommand(request), cancellationToken);
        if (result.Error == AuthenticationErrors.InvalidEmailOrPasswordError)
        {
            return Unauthorized(result.Error);
        }
        
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}