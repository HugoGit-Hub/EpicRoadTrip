using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Roadtrips;

namespace EpicRoadTrip.Application.Roadtrips;

public interface IRoadtripRepository
{
    public Task<Result<Roadtrip>> GetByIdIncludeInstitutionsAndRoutes(int id, CancellationToken cancellationToken);
}