using System.Net;
using EpicRoadTrip.Domain.ErrorHandling;

namespace EpicRoadTrip.Infrastructure.Externals;

internal static class HttpRequestServiceErrors
{
    internal static Error FailedStatusCodeError(HttpStatusCode code) =>
        new($"{nameof(HttpRequestServiceErrors)}.FailedStatusCode", $"Request return the following code : {code}");

    internal static Error NullDeserializationResultError => 
        new($"{nameof(HttpRequestServiceErrors)}.NullDeserializationResult", "The deserialization from json return null result");
}