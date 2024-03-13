using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Routes.GetAllRoute;

public record GetAllRouteQuery : IRequest<Result<IEnumerable<GetAllRouteResponse>>>;