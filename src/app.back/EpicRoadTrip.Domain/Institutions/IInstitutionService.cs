using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicRoadTrip.Domain.Institutions;

public interface IInstitutionService
{
    public Result<IEnumerable<Institution>> GetInstitutionAround(Tuple<float, float> placeCoord, int radius, IEnumerable<InstitutionType> institutionSearchedIds, CancellationToken cancellationToken);
}
