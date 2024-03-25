using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.External;
using EpicRoadTrip.Domain.Institutions;
using EpicRoadTrip.Domain.Routes;
using EpicRoadTrip.Domain.Transportations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicRoadTrip.Application.Institutions;

public class InstitutionService (IExternalInstitutionService externalInstitutionService) : IInstitutionService
{
    public Result<IEnumerable<Institution>> GetInstitutionAround(float lat, float lng, int radius, DateOnly? checkin, DateOnly? checkout, IEnumerable<InstitutionType> institutionSearchedIds, CancellationToken cancellationToken)
    {
        var result = new List<Institution>();
        foreach (var institutionId in institutionSearchedIds)
        {
            switch (institutionId)
            {
                case InstitutionType.Bar:
                    result.AddRange(externalInstitutionService.GetBarAround(lat, lng, radius, cancellationToken).Result.Value);
                    break;
                case InstitutionType.Event:
                    result.AddRange(externalInstitutionService.GetEventAround(lat, lng, radius, cancellationToken).Result.Value);
                    break;
                case InstitutionType.Restaurant:
                    result.AddRange(externalInstitutionService.GetRestaurantAround(lat, lng, radius, cancellationToken).Result.Value);
                    break;
                case InstitutionType.Hotel:
                    result.AddRange(externalInstitutionService.GetHotelAround(lat, lng, radius, checkin, checkout, cancellationToken).Result.Value);
                    break;

                default:
                    throw new Exception("Institution type not recognized");
            }
        }

        return Result<IEnumerable<Institution>>.Success(result);
    }
}
