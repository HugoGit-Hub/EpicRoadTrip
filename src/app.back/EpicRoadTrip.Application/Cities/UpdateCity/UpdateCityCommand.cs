using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Cities.UpdateCity;

public record UpdateCityCommand(UpdateCityRequest Request) : IRequest<Result<UpdateCityResponse>>;