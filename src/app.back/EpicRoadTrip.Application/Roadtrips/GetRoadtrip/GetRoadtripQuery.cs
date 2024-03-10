using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Roadtrips.GetRoadtrip;

public record GetRoadtripQuery(int Id) : IRequest<Result<GetRoadtripResponse>>;