using EpicRoadTrip.Application.Institutions.GetInstitution;
using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Institutions;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Institutions.GetAllInstitution;

public class GetAllInstitutionQueryHandler(IRepository<Institution> repository)
    : IRequestHandler<GetAllInstitutionQuery, Result<IEnumerable<GetInstitutionResponse>>>
{
    public Task<Result<IEnumerable<GetInstitutionResponse>>> Handle(GetAllInstitutionQuery request, CancellationToken cancellationToken)
    {
        var institutions = repository.GetAll();
        if (institutions.IsFailure)
        {
            return Task.FromResult(Result<IEnumerable<GetInstitutionResponse>>.Failure(institutions.Error));
        }

        var response = institutions.Value.Adapt<IEnumerable<GetInstitutionResponse>>();

        return Task.FromResult(Result<IEnumerable<GetInstitutionResponse>>.Success(response));
    }
}