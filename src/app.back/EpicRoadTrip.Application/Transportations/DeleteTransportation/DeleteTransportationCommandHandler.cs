using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Transportations;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Transportations.DeleteTransportation;

public class DeleteTransportationCommandHandler(IRepository<Transportation> repository)
    : IRequestHandler<DeleteTransportationCommand, Result<DeleteTransportationResponse>>
{
    public async Task<Result<DeleteTransportationResponse>> Handle(DeleteTransportationCommand request, CancellationToken cancellationToken)
    {
        var transportation = await repository.GetById(request.Id, cancellationToken);
        if (transportation.IsFailure)
        {
            return Result<DeleteTransportationResponse>.Failure(transportation.Error);
        }

        var delete = await repository.Delete(transportation.Value, cancellationToken);
        var deleteTransportationResponse = delete.Value.Adapt<DeleteTransportationResponse>();
        
        return delete.IsFailure 
            ? Result<DeleteTransportationResponse>.Failure(delete.Error) 
            : Result<DeleteTransportationResponse>.Success(deleteTransportationResponse);
    }
}