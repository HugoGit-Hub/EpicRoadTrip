using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Routes.DeleteRoute;

public record DeleteRouteCommand(int Id) : IRequest<Result<DeleteRouteResponse>>;