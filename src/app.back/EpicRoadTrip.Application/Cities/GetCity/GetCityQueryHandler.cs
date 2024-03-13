using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.Cities;
using EpicRoadTrip.Domain.ErrorHandling;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Cities.GetCity;

public class GetCityQueryHandler(IRepository<City> repository)
    : IRequestHandler<GetCityQuery, Result<GetCityResponse>>
{
    public async Task<Result<GetCityResponse>> Handle(GetCityQuery query, CancellationToken cancellationToken)
    {
        var city = await repository.GetById(query.Id, cancellationToken);
        if (city.IsFailure)
        {
            return Result<GetCityResponse>.Failure(city.Error);
        }

        var response = city.Value.Adapt<GetCityResponse>();

        return Result<GetCityResponse>.Success(response);
    }
}