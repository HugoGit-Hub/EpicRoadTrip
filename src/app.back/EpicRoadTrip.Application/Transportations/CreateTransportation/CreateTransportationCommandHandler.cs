using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Transportations;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Transportations.CreateTransportation;

public class CreateTransportationCommandHandler(IRepository<Transportation> repository)
    : IRequestHandler<CreateTransportationCommand, Result<CreateTransportationResponse>>
{
    public async Task<Result<CreateTransportationResponse>> Handle(CreateTransportationCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var transportation = Transportation.Create(
            0,
            request.Cost,
            request.Score,
            request.Company,
            request.Address,
            request.TransportationType,
            request.RouteId);
        if (transportation.IsFailure)
        {
            return Result<CreateTransportationResponse>.Failure(transportation.Error);
        }

        var create = await repository.Create(transportation.Value, cancellationToken);
        var createTransportationResponse = create.Value.Adapt<CreateTransportationResponse>();
        
        return create.IsFailure 
            ? Result<CreateTransportationResponse>.Failure(create.Error) 
            : Result<CreateTransportationResponse>.Success(createTransportationResponse);
    }
}
