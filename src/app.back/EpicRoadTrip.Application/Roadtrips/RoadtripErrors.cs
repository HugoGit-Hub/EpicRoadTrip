using EpicRoadTrip.Domain.ErrorHandling;

namespace EpicRoadTrip.Application.Roadtrips;

public static class RoadtripErrors
{
    public static Error RoadtripNotFoundError =>
        new($"Roadtrip.{nameof(RoadtripNotFoundError)}", "Unable to find roadtrip");

    public static Error RoadtripNotFoundByIdError =>
        new($"Roadtrip.{nameof(RoadtripNotFoundByIdError)}", "Unable to find roadtrip by id");
}