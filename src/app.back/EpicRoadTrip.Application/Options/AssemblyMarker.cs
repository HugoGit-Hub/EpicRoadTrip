using System.Reflection;

namespace EpicRoadTrip.Application.Options;

public static class AssemblyMarker
{
    public static readonly Assembly Assembly = typeof(AssemblyMarker).Assembly;
}