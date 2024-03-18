using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Authentications.Logins;

public record LoginCommand(LoginRequest Request) : IRequest<Result<LoginResponse>>;