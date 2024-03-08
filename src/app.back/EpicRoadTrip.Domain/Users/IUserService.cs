using EpicRoadTrip.Domain.ErrorHandling;

namespace EpicRoadTrip.Domain.Users;

public interface IUserService
{
    public Task<Result<User>> Create(User user, CancellationToken cancellationToken);

    public Task<Result<User>> GetByEmail(string email, CancellationToken cancellationToken);
}