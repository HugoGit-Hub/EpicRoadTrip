using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.Cities;
using EpicRoadTrip.Domain.ErrorHandling;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Cities.UpdateCity;

public class UpdateCityCommandHandler(IRepository<City> repository)
    : IRequestHandler<UpdateCityCommand, Result<UpdateCityResponse>>
{
    public async Task<Result<UpdateCityResponse>> Handle(UpdateCityCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var city = City.Create(request.Id, request.Name);
        if (city.IsFailure)
        {
            return Result<UpdateCityResponse>.Failure(city.Error);
        }

        var update = await repository.Update(city.Value, cancellationToken);
        if (update.IsFailure)
        {
            return Result<UpdateCityResponse>.Failure(update.Error);
        }

        var response = update.Value.Adapt<UpdateCityResponse>();

        return Result<UpdateCityResponse>.Success(response);
    }
}