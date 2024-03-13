using EpicRoadTrip.Domain.CurrentUsers;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Users;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using EpicRoadTrip.Application.Users;

namespace EpicRoadTrip.Application.CurrentUsers;

public class CurrentUserService(
    IHttpContextAccessor httpContextAccessor,
    IUserRepository userRepository) 
    : ICurrentUserService
{
    private ClaimsPrincipal CurrentUser => httpContextAccessor.HttpContext.User;

    public async Task<Result<User>> GetCurrentUser(CancellationToken cancellationToken)
    {
        var email = GetClaim(ClaimTypes.Email);
        if (email.IsFailure)
        {
            return Result<User>.Failure(email.Error);
        }

        var currentUser = await userRepository.GetByEmailIncludRoadtrips(email.Value, cancellationToken);
        if (currentUser.IsFailure)
        {
            return Result<User>.Failure(currentUser.Error);
        }
        
        return currentUser.IsFailure 
            ? Result<User>.Failure(currentUser.Error) 
            : Result<User>.Success(currentUser.Value);
    }

    private Result<string> GetClaim(string claimType)
    {
        var result = CurrentUser.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;

        return result is null
            ? Result<string>.Failure(CurrentUserErrors.ClaimTypeNullError)
            : Result<string>.Success(result);
    }
}