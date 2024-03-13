using EpicRoadTrip.Application.Cities.GetCity;
using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.Cities;
using EpicRoadTrip.Domain.ErrorHandling;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Cities.GetAllCity;

public class GetAllCityQueryHandler(IRepository<City> repository)
    : IRequestHandler<GetAllCityQuery, Result<IEnumerable<GetCityResponse>>>
{
    public Task<Result<IEnumerable<GetCityResponse>>> Handle(GetAllCityQuery query, CancellationToken cancellationToken)
    {
        var cities = repository.GetAll();
        if (cities.IsFailure)
        {
            return Task.FromResult(Result<IEnumerable<GetCityResponse>>.Failure(cities.Error));
        }

        var reponse = cities.Value.Adapt<IEnumerable<GetCityResponse>>();

        return Task.FromResult(Result<IEnumerable<GetCityResponse>>.Success(reponse));
    }
}