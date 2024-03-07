using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Authentications.Registers;

public record RegisterCommand(RegisterRequest Request) : IRequest<Result<string>>;