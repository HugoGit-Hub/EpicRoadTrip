namespace EpicRoadTrip.Application.Options;

public sealed class Jwt
{
    public string Issuer { get; init; } = null!;

    public string Audience { get; init; } = null!;

    public string Key { get; init; } = null!;
}