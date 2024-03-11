using EpicRoadTrip.Application.Roadtrips.GetRoadtrip;
using EpicRoadTrip.Application.Routes.GetRoute;
using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Routes.GetRouteBetweenPoints;

public record GetRouteBetweenPointsQuery(GetRouteRequest request) : IRequest<Result<IEnumerable<GetRouteResponse>>>;
