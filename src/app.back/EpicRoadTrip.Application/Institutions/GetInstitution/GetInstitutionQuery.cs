using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Institutions.GetInstitution;

public record GetInstitutionQuery(int Id) : IRequest<Result<GetInstitutionResponse>>;