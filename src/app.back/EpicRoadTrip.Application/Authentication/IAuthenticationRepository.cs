namespace EpicRoadTrip.Application.Authentication;

public interface IAuthenticationRepository
{
    Task<bool> IsEmailAlreadyUse(string email, CancellationToken cancellationToken);
}