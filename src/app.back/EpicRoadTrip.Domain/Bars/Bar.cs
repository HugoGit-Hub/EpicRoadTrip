using EpicRoadTrip.Domain.Institutions;

namespace EpicRoadTrip.Domain.Bars;

public sealed class Bar : Institution
{
    public int Id { get; }

    private Bar(
        int id,
        string name,
        double? price,
        string? phoneNumber,
        string? email,
        string address,
        int cityId) 
        : base(name, price, phoneNumber, email, address, cityId)
    {
        Id = id;
    }

    public static Bar Create(
        int id,
        string name,
        double? price,
        string? phoneNumber,
        string? email,
        string address, 
        int cityId)
    {
        return new Bar(id, name, price, phoneNumber, email, address, cityId);
    }
}