using EpicRoadTrip.Application.Transportations.GetTransportation;
using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Transportations.GetAllTransportation;

public record GetAllTransportationQuery : IRequest<Result<IEnumerable<GetTransportationResponse>>>;