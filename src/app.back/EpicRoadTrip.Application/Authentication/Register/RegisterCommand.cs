using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Authentication.Register;

public record RegisterCommand(RegisterRequest Request) : IRequest<Result<string>>;