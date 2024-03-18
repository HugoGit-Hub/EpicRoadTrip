using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Routes;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Routes.UpdateRoute;

public class UpdateRouteCommandHandler(IRepository<Route> repository)
    : IRequestHandler<UpdateRouteCommand, Result<UpdateRouteResponse>>
{
    public async Task<Result<UpdateRouteResponse>> Handle(UpdateRouteCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var timeSpan = TimeSpan.FromTicks(request.Duration);
        var route = Route.Create(
            request.Id,
            request.Distance,
            timeSpan,
            request.CityOneName,
            request.CityTwoName,
            request.RoadtripId,
            "geoJson");
        if (route.IsFailure)
        {
            return Result<UpdateRouteResponse>.Failure(route.Error);
        }

        var update = await repository.Update(route.Value, cancellationToken);
        var updateRouteResponse = update.Value.Adapt<UpdateRouteResponse>();
        
        return update.IsFailure 
            ? Result<UpdateRouteResponse>.Failure(update.Error) 
            : Result<UpdateRouteResponse>.Success(updateRouteResponse);
    }
}