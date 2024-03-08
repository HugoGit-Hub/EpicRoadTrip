namespace EpicRoadTrip.Application.Authentications;

public interface IAuthenticationRepository
{
    Task<bool> IsEmailAlreadyUse(string email, CancellationToken cancellationToken);

    Task<bool> AreCredentialscorrects(string email, string password, CancellationToken cancellationToken);
}