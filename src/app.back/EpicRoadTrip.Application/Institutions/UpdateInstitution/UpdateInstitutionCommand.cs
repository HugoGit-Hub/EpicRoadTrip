using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Institutions.UpdateInstitution;

public record UpdateInstitutionCommand(UpdateInstitutionRequest Request) : IRequest<Result<UpdateInstitutionResponse>>;