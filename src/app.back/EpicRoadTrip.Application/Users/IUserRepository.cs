using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Users;

namespace EpicRoadTrip.Application.Users;

public interface IUserRepository
{
    public Task<Result<User>> GetByEmailIncludRoadtrips(string email, CancellationToken cancellationToken);
}