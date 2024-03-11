using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Roadtrips.UpdateRoadtrip;

public record UpdateRoadtripCommand(UpdateRoadtripRequest Request) : IRequest<Result<UpdateRoadtripResponse>>;