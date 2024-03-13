using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Transportations.DeleteTransportation;

public record DeleteTransportationCommand(int Id) : IRequest<Result<DeleteTransportationResponse>>;