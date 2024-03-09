using EpicRoadTrip.Domain.ErrorHandling;

namespace EpicRoadTrip.Application.Repositories;

public static class RepositoryErrors
{
    private const string Repository = "Repository";

    private static string ExceptionOccur(Exception e) => $"This exception occur : {e}";

    public static Error FailedToCreateError(Exception e) =>
        new($"{Repository}.{nameof(FailedToCreateError)}", ExceptionOccur(e));

    public static Error FailedToUpdateError(Exception e) =>
        new($"{Repository}.{nameof(FailedToUpdateError)}", ExceptionOccur(e));

    public static Error FailedToDeleteError(Exception e) =>
        new($"{Repository}.{nameof(FailedToDeleteError)}", ExceptionOccur(e));

    public static Error FailedToGetByIdError(Exception e) =>
        new($"{Repository}.{nameof(FailedToGetByIdError)}", ExceptionOccur(e));
    
    public static Error FailedToGetByIdError() =>
        new($"{Repository}.{nameof(FailedToGetByIdError)}", "No lines match with given Id");

    public static Error FailToGetAllError(Exception e) =>
        new($"{Repository}.{nameof(FailToGetAllError)}", ExceptionOccur(e));
}