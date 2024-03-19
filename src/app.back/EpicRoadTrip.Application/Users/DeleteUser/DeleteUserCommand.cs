using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Users.DeleteUser;

public record DeleteUserCommand(int Id) : IRequest<Result<DeleteUserResponse>>;