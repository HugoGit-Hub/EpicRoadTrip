using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.Cities;
using EpicRoadTrip.Domain.ErrorHandling;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Cities.DeleteCity;

public class DeleteCityCommandHandler(IRepository<City> repository)
    : IRequestHandler<DeleteCityCommand, Result<DeleteCityResponse>>
{
    public async Task<Result<DeleteCityResponse>> Handle(DeleteCityCommand command, CancellationToken cancellationToken)
    {
        var city = await repository.GetById(command.Id,cancellationToken);
        if (city.IsFailure)
        {
            return Result<DeleteCityResponse>.Failure(city.Error);
        }

        var delete = await repository.Delete(city.Value, cancellationToken);
        if (delete.IsFailure)
        {
            return Result<DeleteCityResponse>.Failure(delete.Error);
        }

        var response = delete.Value.Adapt<DeleteCityResponse>();

        return Result<DeleteCityResponse>.Success(response);
    }
}