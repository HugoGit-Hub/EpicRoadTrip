using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.Cities;
using EpicRoadTrip.Domain.ErrorHandling;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Cities.CreateCity;

public class CreateCityCommandHandler(IRepository<City> repository)
    : IRequestHandler<CreateCityCommand, Result<CreateCityResponse>>
{
    public async Task<Result<CreateCityResponse>> Handle(CreateCityCommand command, CancellationToken cancellationToken)
    {
        var city = City.Create(0, command.Name);
        if (city.IsFailure)
        {
            return Result<CreateCityResponse>.Failure(city.Error);
        }

        var create = await repository.Create(city.Value, cancellationToken);
        if (create.IsFailure)
        {
            return Result<CreateCityResponse>.Failure(create.Error);
        }

        var response = create.Value.Adapt<CreateCityResponse>();

        return Result<CreateCityResponse>.Success(response);
    }
}