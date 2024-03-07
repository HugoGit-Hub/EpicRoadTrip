using EpicRoadTrip.Domain.ErrorHandling;

namespace EpicRoadTrip.Domain.Users;

public interface IUserService
{
    public Task<Result<User>> Create(User user, CancellationToken cancellationToken);
}