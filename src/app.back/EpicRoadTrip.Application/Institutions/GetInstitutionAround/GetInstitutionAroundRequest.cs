using EpicRoadTrip.Domain.Institutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicRoadTrip.Application.Institutions.GetInstitutionAround;

public record GetInstitutionAroundRequest
{
    public required float Lat { get; init; }
    public required float Lng { get; init; }
    public DateOnly? Checkin { get; init; }
    public DateOnly? Checkout { get; init; }

    public required int Radius { get; init; }

    public required List<InstitutionType> InstitutionTypes { get; init; }

}