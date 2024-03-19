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
    public Task<Result<IEnumerable<Institution>>> GetBarAround(Tuple<float, float> placeCoord, int radius, CancellationToken cancellationToken);
    public Task<Result<IEnumerable<Institution>>> GetHotelAround(Tuple<float, float> placeCoord, int radius, CancellationToken cancellationToken);
    public Task<Result<IEnumerable<Institution>>> GetRestaurantAround(Tuple<float, float> placeCoord, int radius, CancellationToken cancellationToken);
    public Task<Result<IEnumerable<Institution>>> GetEventAround(Tuple<float, float> placeCoord, int radius, CancellationToken cancellationToken);
}
