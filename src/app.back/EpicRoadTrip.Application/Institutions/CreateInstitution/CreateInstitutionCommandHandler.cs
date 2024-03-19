using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Institutions;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Institutions.CreateInstitution;

public class CreateInstitutionCommandHandler(IRepository<Institution> repository)
    : IRequestHandler<CreateInstitutionCommand, Result<CreateInstitutionResponse>>
{
    public async Task<Result<CreateInstitutionResponse>> Handle(CreateInstitutionCommand command, CancellationToken cancellationToken)
    {
        var resquest = command.Request;
        var institution = Institution.Create(
            0,
            resquest.Name,
            resquest.Price,
            resquest.PhoneNumber,
            resquest.Email,
            resquest.Address,
            resquest.Type,
            resquest.RoadTripId,
            resquest.WebSite,
            resquest.Coord);
        if (institution.IsFailure)
        {
            return Result<CreateInstitutionResponse>.Failure(institution.Error);
        }

        var create = await repository.Create(institution.Value, cancellationToken);
        if (create.IsFailure)
        {
            return Result<CreateInstitutionResponse>.Failure(create.Error);
        }

        var response = create.Value.Adapt<CreateInstitutionResponse>();

        return Result<CreateInstitutionResponse>.Success(response);
    }
}