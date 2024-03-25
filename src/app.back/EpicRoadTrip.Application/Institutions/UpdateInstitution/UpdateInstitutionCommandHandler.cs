using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Institutions;
using Mapster;
using MediatR;

namespace EpicRoadTrip.Application.Institutions.UpdateInstitution;

public class UpdateInstitutionCommandHandler(IRepository<Institution> repository)
    : IRequestHandler<UpdateInstitutionCommand, Result<UpdateInstitutionResponse>>
{
    public async Task<Result<UpdateInstitutionResponse>> Handle(UpdateInstitutionCommand request, CancellationToken cancellationToken)
    {
        var institution = Institution.Create(
            request.Request.Id,
            request.Request.Name,
            request.Request.Price,
            request.Request.PhoneNumber,
            request.Request.Email,
            request.Request.Address,
            request.Request.Type,
            request.Request.RoadTripId,
            request.Request.WebSite,
            request.Request.Lat,
            request.Request.Lng,
            request.Request.PreviewUrl);
        if (institution.IsFailure)
        {
            return Result<UpdateInstitutionResponse>.Failure(institution.Error);
        }

        var update = await repository.Update(institution.Value, cancellationToken);
        if (update.IsFailure)
        {
            return Result<UpdateInstitutionResponse>.Failure(update.Error);
        }

        var response = update.Value.Adapt<UpdateInstitutionResponse>();

        return Result<UpdateInstitutionResponse>.Success(response);
    }
}