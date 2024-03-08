using EpicRoadTrip.Application.Users;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Users;
using EpicRoadTrip.Infrastructure.Context;

namespace EpicRoadTrip.Infrastructure.Users;

public class UserRepository(EpicRoadTripContext context) : IUserRepository
{
    public async Task<Result<User>> Create(User user, CancellationToken cancellationToken)
    {
        var create = await context.Users.AddAsync(user, cancellationToken);
        try
        {
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            return Result<User>.Failure(UserErrors.ContextFailedToCreateUser(e));
        }

        return Result<User>.Success(create.Entity);
    }
}