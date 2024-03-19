using EpicRoadTrip.Application.Institutions.GetAllInstitution;
using EpicRoadTrip.Application.Institutions.GetInstitution;
using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Application.Routes.GetRoute;
using EpicRoadTrip.Application.Routes;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Institutions;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;

namespace EpicRoadTrip.Application.Institutions.GetInstitutionAround;

public class GetInstitutionAroundQueryHandler(
    IInstitutionService institutionService)
    : IRequestHandler<GetInstitutionAroundQuery, Result<IEnumerable<GetInstitutionResponse>>>
{
    public Task<Result<IEnumerable<GetInstitutionResponse>>> Handle(GetInstitutionAroundQuery request, CancellationToken cancellationToken)
    {
        if (request.query.InstitutionTypes.Any()
            && request.query.PlaceCoord.Item1 != default
            && request.query.PlaceCoord.Item2 != default
            && request.query.Radius > 0)
        {
            var result = new List<GetInstitutionResponse>();
            var institution = institutionService.GetInstitutionAround(request.query.PlaceCoord, request.query.Radius, request.query.InstitutionTypes, cancellationToken);
            
            foreach (var inst in institution.Value)
            {
                result.Add(inst.Adapt<GetInstitutionResponse>());
            }

            return Task.FromResult(Result<IEnumerable<GetInstitutionResponse>>.Success(result));
        }
        else
        {
            return Task.FromResult(Result<IEnumerable<GetInstitutionResponse>>.Failure(new Error("999", "Undefined message error")));
        }
    }
}
