using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Roadtrips;
using EpicRoadTrip.Domain.Users;

namespace EpicRoadTrip.Application.Roadtrips;

public class RoadtripService : IRoadtripService
{
    public Task<Result<Roadtrip>> GetUserRoadtrip(int id, User user, CancellationToken cancellationToken)
    {
        var roadtrip = user.Roadtrips.FirstOrDefault(r => r.Id == id);

        return roadtrip is null
            ? Task.FromResult(Result<Roadtrip>.Failure(RoadtripErrors.RoadtripNotFoundError))
            : Task.FromResult(Result<Roadtrip>.Success(roadtrip));
    }
}