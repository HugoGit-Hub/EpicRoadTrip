using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Cities.CreateCity;

public record CreateCityCommand(string Name) : IRequest<Result<CreateCityResponse>>;