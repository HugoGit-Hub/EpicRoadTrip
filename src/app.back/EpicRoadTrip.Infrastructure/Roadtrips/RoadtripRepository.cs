using EpicRoadTrip.Application.Roadtrips;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Roadtrips;
using EpicRoadTrip.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EpicRoadTrip.Infrastructure.Roadtrips;

public class RoadtripRepository(EpicRoadTripContext context) : IRoadtripRepository
{
    public async Task<Result<Roadtrip>> GetByIdIncludeInstitutionsAndRoutes(int id, CancellationToken cancellationToken)
    {
        var roadtrip = await context.Roadtrips
            .AsNoTracking()
            .Include(r => r.Institutions)
            .Include(r => r.Routes)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

        return roadtrip is null
            ? Result<Roadtrip>.Failure(RoadtripErrors.RoadtripNotFoundByIdError)
            : Result<Roadtrip>.Success(roadtrip);
    }
}