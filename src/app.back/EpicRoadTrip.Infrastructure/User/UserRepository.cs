using EpicRoadTrip.Application.Users;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Users;
using EpicRoadTrip.Infrastructure.Context;

namespace EpicRoadTrip.Infrastructure.User;

public class UserRepository(EpicRoadTripContext context) : IUserRepository
{
    public async Task<Result<Domain.Users.User>> Create(Domain.Users.User user, CancellationToken cancellationToken)
    {
        var create = await context.Users.AddAsync(user, cancellationToken);
        try
        {
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            return Result<Domain.Users.User>.Failure(UserErrors.ContextFailedToCreateUser(e));
        }

        return Result<Domain.Users.User>.Success(create.Entity);
    }
}