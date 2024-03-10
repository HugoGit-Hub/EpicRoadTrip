using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Users;

namespace EpicRoadTrip.Domain.CurrentUsers;

public interface ICurrentUserService
{
    public Task<Result<User>> GetCurrentUser(CancellationToken cancellationToken);
}