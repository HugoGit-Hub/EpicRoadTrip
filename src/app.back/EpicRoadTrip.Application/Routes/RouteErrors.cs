using EpicRoadTrip.Domain.ErrorHandling;

namespace EpicRoadTrip.Application.Routes;

public static class RouteErrors
{
    public const string Route = "Route";

    public static Error ConvertionToTimeSpanFailed =>
        new($"{Route}.{nameof(ConvertionToTimeSpanFailed)}", "The conversion to TimeSpan failed.");
}