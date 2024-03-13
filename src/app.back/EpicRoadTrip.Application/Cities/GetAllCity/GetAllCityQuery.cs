using EpicRoadTrip.Application.Cities.GetCity;
using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Cities.GetAllCity;

public record GetAllCityQuery : IRequest<Result<IEnumerable<GetCityResponse>>>;