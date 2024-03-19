using EpicRoadTrip.Application.Institutions.GetInstitution;
using EpicRoadTrip.Domain.ErrorHandling;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicRoadTrip.Application.Institutions.GetInstitutionAround;

public record GetInstitutionAroundQuery(GetInstitutionAroundRequest query) : IRequest<Result<IEnumerable<GetInstitutionResponse>>>;
