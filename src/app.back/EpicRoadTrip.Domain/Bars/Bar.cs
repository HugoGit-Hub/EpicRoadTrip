using EpicRoadTrip.Domain.Institutions;

namespace EpicRoadTrip.Domain.Bars;

public sealed class Bar : Institution
{
    public int Id { get; init; }

    private Bar(
        string name,
        double? price,
        string? phoneNumber,
        string? email,
        string address,
        int cityId) 
        : base(name, price, phoneNumber, email, address, cityId)
    {
    }

    public static Bar Create(
        string name,
        double? price,
        string? phoneNumber,
        string? email,
        string address, 
        int cityId)
    {
        return new Bar(name, price, phoneNumber, email, address, cityId);
    }
}