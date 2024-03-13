using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Routes.CreateRoute;

public record CreateRouteCommand(CreateRouteRequest Request) : IRequest<Result<CreateRouteResponse>>;