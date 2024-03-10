using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Roadtrips.CreateRoadtrip;

public record CreateRoadtripCommand(CreateRoadtripRequest Request) : IRequest<Result<CreateRoadtripResponse>>;