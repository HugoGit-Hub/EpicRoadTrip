using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Roadtrips.GetAllRoadtripInformations;

public record GetRoadtripInformationsQuery(int Id) : IRequest<Result<GetRoadtripInformationsResponse>>;