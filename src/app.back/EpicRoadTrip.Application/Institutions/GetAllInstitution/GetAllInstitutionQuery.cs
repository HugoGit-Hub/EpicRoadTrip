using EpicRoadTrip.Application.Institutions.GetInstitution;
using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Institutions.GetAllInstitution;

public record GetAllInstitutionQuery : IRequest<Result<IEnumerable<GetInstitutionResponse>>>;