using EpicRoadTrip.Application.Roadtrips.GetRoadtrip;
using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Roadtrips.GetAllRoadtrip;

public record GetAllRoadtripQuery : IRequest<Result<IEnumerable<GetRoadtripResponse>>>;