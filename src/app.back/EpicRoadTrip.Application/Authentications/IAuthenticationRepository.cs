namespace EpicRoadTrip.Application.Authentications;

public interface IAuthenticationRepository
{
    Task<bool> IsEmailAlreadyUse(string email, CancellationToken cancellationToken);
}