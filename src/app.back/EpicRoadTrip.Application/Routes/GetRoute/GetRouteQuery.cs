using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Routes.GetRoute;

public record GetRouteQuery(int Id) : IRequest<Result<GetRouteResponse>>;