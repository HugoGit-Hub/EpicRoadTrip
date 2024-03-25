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
    : IRequestHandler<GetInstitutionAroundQuery, Result<IEnumerable<GetInstitutionAroundResponse>>>
{
    public Task<Result<IEnumerable<GetInstitutionAroundResponse>>> Handle(GetInstitutionAroundQuery request, CancellationToken cancellationToken)
    {
        if (request.query.InstitutionTypes.Any()
            && request.query.Lat != default
            && request.query.Lng != default
            && request.query.Radius > 0)
        {
            var result = new List<GetInstitutionAroundResponse>();
            var institution = institutionService.GetInstitutionAround(request.query.Lat, request.query.Lng, request.query.Radius, request.query.Checkin, request.query.Checkout, request.query.InstitutionTypes, cancellationToken);
            
            foreach (var inst in institution.Value)
            {
                result.Add(inst.Adapt<GetInstitutionAroundResponse>());
            }

            return Task.FromResult(Result<IEnumerable<GetInstitutionAroundResponse>>.Success(result));
        }
        else
        {
            return Task.FromResult(Result<IEnumerable<GetInstitutionAroundResponse>>.Failure(new Error("999", "Undefined message error")));
        }
    }
}
