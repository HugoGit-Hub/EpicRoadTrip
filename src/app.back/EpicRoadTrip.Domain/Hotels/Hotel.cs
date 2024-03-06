using EpicRoadTrip.Domain.Institutions;

namespace EpicRoadTrip.Domain.Hotels;

public sealed class Hotel : Institution
{
    public int Id { get; init; }

    private Hotel(
        string name,
        double? price,
        string? phoneNumber,
        string? email,
        string address,
        int cityId) 
        : base(name, price, phoneNumber, email, address, cityId)
    {
    }

    public static Hotel Create(
        string name,
        double price,
        string phoneNumber,
        string email,
        string address,
        int cityId)
    {
        return new Hotel(
            name,
            price,
            phoneNumber,
            email,
            address,
            cityId);
    }
}