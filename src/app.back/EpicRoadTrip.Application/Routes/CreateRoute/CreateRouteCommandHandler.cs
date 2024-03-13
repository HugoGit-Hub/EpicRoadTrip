using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Routes;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Routes.CreateRoute;

public class CreateRouteCommandHandler(IRepository<Route> repository)
    : IRequestHandler<CreateRouteCommand, Result<CreateRouteResponse>>
{
    public async Task<Result<CreateRouteResponse>> Handle(CreateRouteCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var timeSpan = TimeSpan.FromTicks(request.Duration);
        var route = Route.Create(
            0,
            request.Distance,
            timeSpan,
            request.CityOneId,
            request.CityTwoId,
            request.RoadtripId);
        if (route.IsFailure)
        {
            return Result<CreateRouteResponse>.Failure(route.Error);
        }

        var create = await repository.Create(route.Value, cancellationToken);
        var createRouteResponse = create.Value.Adapt<CreateRouteResponse>();
        
        return create.IsFailure 
            ? Result<CreateRouteResponse>.Failure(create.Error)
            : Result<CreateRouteResponse>.Success(createRouteResponse);
    }
}