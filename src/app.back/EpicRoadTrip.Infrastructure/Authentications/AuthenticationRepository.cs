using EpicRoadTrip.Application.Authentications;
using EpicRoadTrip.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EpicRoadTrip.Infrastructure.Authentications;

public class AuthenticationRepository(EpicRoadTripContext context) : IAuthenticationRepository
{
    public async Task<bool> IsEmailAlreadyUse(string email, CancellationToken cancellationToken)
    {
        return await context.Users.AnyAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<bool> AreCredentialscorrects(string email, string password, CancellationToken cancellationToken)
    {
        return await context.Users.AnyAsync(u => u.Email == email && u.Password == password, cancellationToken);
    }
}