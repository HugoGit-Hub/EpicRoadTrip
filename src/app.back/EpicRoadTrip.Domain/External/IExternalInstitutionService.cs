using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Institutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicRoadTrip.Domain.External;

public interface IExternalInstitutionService
{
    public Task<Result<IEnumerable<Institution>>> GetBarAround(float lat, float lng, int radius, CancellationToken cancellationToken);
    public Task<Result<IEnumerable<Institution>>> GetHotelAround(float lat, float lng, int radius, DateOnly? checkin, DateOnly? checkout, CancellationToken cancellationToken);
    public Task<Result<IEnumerable<Institution>>> GetRestaurantAround(float lat, float lng, int radius, CancellationToken cancellationToken);
    public Task<Result<IEnumerable<Institution>>> GetEventAround(float lat, float lng, int radius, CancellationToken cancellationToken);
}
