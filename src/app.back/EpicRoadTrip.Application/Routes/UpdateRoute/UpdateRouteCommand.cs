using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Routes.UpdateRoute;

public record UpdateRouteCommand(UpdateRouteRequest Request) : IRequest<Result<UpdateRouteResponse>>;