using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Users.UpdateUser;

public record UpdateUserCommand(UpdateUserRequest Request) : IRequest<Result<UpdateUserResponse>>;