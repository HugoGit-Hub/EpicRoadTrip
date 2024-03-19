using EpicRoadTrip.Application.Users.DeleteUser;
using EpicRoadTrip.Application.Users.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EpicRoadTrip.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(ISender sender) : Controller
{
    [Authorize]
    [HttpPut(nameof(Update))]
    public async Task<ActionResult<UpdateUserResponse>> Update(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new UpdateUserCommand(request), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpDelete(nameof(Delete))]
    public async Task<ActionResult<DeleteUserResponse>> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteUserCommand(id), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}