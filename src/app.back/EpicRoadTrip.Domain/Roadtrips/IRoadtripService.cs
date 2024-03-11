using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Users;

namespace EpicRoadTrip.Domain.Roadtrips;

public interface IRoadtripService
{
    Task<Result<Roadtrip>> GetUserRoadtrip(int id, User user, CancellationToken cancellationToken);
}