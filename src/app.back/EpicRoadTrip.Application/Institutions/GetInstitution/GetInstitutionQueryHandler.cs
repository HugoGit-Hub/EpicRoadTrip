using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Institutions;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Institutions.GetInstitution;

public class GetInstitutionQueryHandler(IRepository<Institution> repository)
    : IRequestHandler<GetInstitutionQuery, Result<GetInstitutionResponse>>
{
    public async Task<Result<GetInstitutionResponse>> Handle(GetInstitutionQuery query, CancellationToken cancellationToken)
    {
        var institution = await repository.GetById(query.Id, cancellationToken);
        if (institution.IsFailure)
        {
            return Result<GetInstitutionResponse>.Failure(institution.Error);
        }

        var response = institution.Value.Adapt<GetInstitutionResponse>();

        return Result<GetInstitutionResponse>.Success(response);
    }
}