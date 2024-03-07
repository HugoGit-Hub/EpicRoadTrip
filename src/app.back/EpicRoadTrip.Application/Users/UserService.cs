using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Users;

namespace EpicRoadTrip.Application.Users;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<Result<User>> Create(User user, CancellationToken cancellationToken)
    {
        var create = await userRepository.Create(user, cancellationToken);
        
        return create.IsFailure 
            ? Result<User>.Failure(create.Error) 
            : Result<User>.Success(create.Value);
    }
}