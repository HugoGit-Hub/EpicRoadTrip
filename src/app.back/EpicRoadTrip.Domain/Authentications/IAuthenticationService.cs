using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Users;

namespace EpicRoadTrip.Domain.Authentications;

public interface IAuthenticationService
{
    public Task<bool> IsEmailAlreadyUse(string email, CancellationToken cancellationToken);

    public Result<string> Encrypt(string content);

    public Result<string> HashWithSalt(string content);

    public Result<string> GenerateToken(User user);
}