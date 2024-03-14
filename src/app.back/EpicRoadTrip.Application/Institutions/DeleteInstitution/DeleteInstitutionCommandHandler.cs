using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Institutions;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Institutions.DeleteInstitution;

public class DeleteInstitutionCommandHandler(IRepository<Institution> repository)
    : IRequestHandler<DeleteInstitutionCommand, Result<DeleteInstitutionResponse>>
{
    public async Task<Result<DeleteInstitutionResponse>> Handle(DeleteInstitutionCommand command, CancellationToken cancellationToken)
    {
        var institution = await repository.GetById(command.Id, cancellationToken);
        if (institution.IsFailure)
        {
            return Result<DeleteInstitutionResponse>.Failure(institution.Error);
        }

        var delete = await repository.Delete(institution.Value, cancellationToken);
        if (delete.IsFailure)
        {
            return Result<DeleteInstitutionResponse>.Failure(delete.Error);
        }

        var response = delete.Value.Adapt<DeleteInstitutionResponse>();

        return Result<DeleteInstitutionResponse>.Success(response);
    }
}