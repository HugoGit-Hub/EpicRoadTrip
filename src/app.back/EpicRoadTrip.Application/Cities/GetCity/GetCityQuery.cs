using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Cities.GetCity;

public record GetCityQuery(int Id) : IRequest<Result<GetCityResponse>>;