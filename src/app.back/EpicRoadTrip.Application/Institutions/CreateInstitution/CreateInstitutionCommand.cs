using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Institutions.CreateInstitution;

public record CreateInstitutionCommand(CreateInstitutionRequest Request) : IRequest<Result<CreateInstitutionResponse>>;