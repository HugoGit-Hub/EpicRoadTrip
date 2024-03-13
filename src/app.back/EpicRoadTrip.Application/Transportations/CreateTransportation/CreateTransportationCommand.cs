using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Transportations.CreateTransportation;

public record CreateTransportationCommand(CreateTransportationRequest Request) : IRequest<Result<CreateTransportationResponse>>;