using EpicRoadTrip.Domain.Institutions;

namespace EpicRoadTrip.Domain.Restaurants;

public sealed class Restaurant : Institution
{
    public int Id { get; init; }

    private Restaurant(
        string name,
        double? price,
        string? phoneNumber,
        string? email,
        string address,
        int cityId) 
        : base(name, price, phoneNumber, email, address, cityId)
    {
    }

    public static Restaurant Create(
        string name, 
        double? price, 
        string? phoneNumber, 
        string? email, 
        string address, 
        int cityId)
    {
        return new Restaurant(name, price, phoneNumber, email, address, cityId);
    }
}