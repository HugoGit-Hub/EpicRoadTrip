using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;

namespace EpicRoadTrip.Application.Institutions.DeleteInstitution;

public record DeleteInstitutionCommand(int Id) : IRequest<Result<DeleteInstitutionResponse>>;