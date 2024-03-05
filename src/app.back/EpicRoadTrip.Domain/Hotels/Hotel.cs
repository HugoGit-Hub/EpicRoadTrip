using EpicRoadTrip.Domain.Institutions;

namespace EpicRoadTrip.Domain.Hotels;

public sealed class Hotel : Institution
{
    public int Id { get; }

    private Hotel(
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

    public static Hotel Create(
        int id,
        string name,
        double price,
        string phoneNumber,
        string email,
        string address,
        int cityId)
    {
        return new Hotel(
            id,
            name,
            price,
            phoneNumber,
            email,
            address,
            cityId);
    }
}