using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.ErrorHandling;
using Mapster;
using MediatR;
using Route = EpicRoadTrip.Domain.Routes.Route;

namespace EpicRoadTrip.Application.Routes.DeleteRoute;

public class DeleteRouteCommandHandler(IRepository<Route> repository)
    : IRequestHandler<DeleteRouteCommand, Result<DeleteRouteResponse>>
{
    public async Task<Result<DeleteRouteResponse>> Handle(DeleteRouteCommand command, CancellationToken cancellationToken)
    {
        var route = await repository.GetById(command.Id, cancellationToken);
        if (route.IsFailure)
        {
            return Result<DeleteRouteResponse>.Failure(route.Error);
        }

        var delete = await repository.Delete(route.Value, cancellationToken);
        var deleteRouteResponse = delete.Value.Adapt<DeleteRouteResponse>();

        return delete.IsFailure 
            ? Result<DeleteRouteResponse>.Failure(delete.Error)
            : Result<DeleteRouteResponse>.Success(deleteRouteResponse);
    }
}