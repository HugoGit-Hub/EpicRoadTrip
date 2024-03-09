using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Users;

namespace EpicRoadTrip.Application.Users;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<Result<User>> GetByEmail(string email, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmail(email, cancellationToken);

        return user.IsFailure
            ? Result<User>.Failure(user.Error)
            : Result<User>.Success(user.Value);
    }
}