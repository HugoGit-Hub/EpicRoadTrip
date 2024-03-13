using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Transportations.UpdateTransportation;

public record UpdateTransportationCommand(UpdateTransportationRequest Request) : IRequest<Result<UpdateTransportationResponse>>;