using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Roadtrips.DeleteRoadtrip;

public record DeleteRoadtripCommand(int Id) : IRequest<Result<DeleteRoadtripResponse>>;