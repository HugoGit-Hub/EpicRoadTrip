using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Cities.DeleteCity;

public record DeleteCityCommand(int Id) : IRequest<Result<DeleteCityResponse>>;