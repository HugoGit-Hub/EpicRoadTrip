namespace EpicRoadTrip.Domain.ErrorHandling.Generics;

public class GenericErrors<T> where T : class
{
    public static Error InvalidFormatError(Exception e) =>
        new($"Generic.{nameof(InvalidFormatError)}", $"{typeof(T).Name} has invalid format, following exception throw : {e}");
}