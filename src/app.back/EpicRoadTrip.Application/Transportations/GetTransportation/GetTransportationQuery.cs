using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Transportations.GetTransportation;

public record GetTransportationQuery(int Id) : IRequest<Result<GetTransportationResponse>>;