using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Transportations;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Transportations.UpdateTransportation;

public class UpdateTransportationCommandHandler(IRepository<Transportation> repository)
    : IRequestHandler<UpdateTransportationCommand, Result<UpdateTransportationResponse>>
{
    public async Task<Result<UpdateTransportationResponse>> Handle(UpdateTransportationCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var transportation = Transportation.Create(
            request.Id,
            request.Cost,
            request.Score,
            request.Company,
            request.Address,
            request.TransportationType,
            request.RouteId);
        if (transportation.IsFailure)
        {
            return Result<UpdateTransportationResponse>.Failure(transportation.Error);
        }

        var update = await repository.Update(transportation.Value, cancellationToken);
        var updateTransportationResponse = update.Value.Adapt<UpdateTransportationResponse>();
        
        return update.IsFailure 
            ? Result<UpdateTransportationResponse>.Failure(update.Error) 
            : Result<UpdateTransportationResponse>.Success(updateTransportationResponse);
    }
}