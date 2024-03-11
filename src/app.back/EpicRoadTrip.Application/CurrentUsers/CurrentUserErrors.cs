using EpicRoadTrip.Domain.ErrorHandling;

namespace EpicRoadTrip.Application.CurrentUsers;

public static class CurrentUserErrors
{
    private const string CurrentUser = "CurrentUser";

    public static readonly Error ClaimTypeNullError = new(
        $"{nameof(CurrentUser)}.{nameof(ClaimTypeNullError)}", "Claim type provided is null");
}