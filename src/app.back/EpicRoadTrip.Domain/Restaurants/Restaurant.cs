using EpicRoadTrip.Domain.Institutions;

namespace EpicRoadTrip.Domain.Restaurants;

public sealed class Restaurant : Institution
{
    public int Id { get; }

    private Restaurant(
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

    public static Restaurant Create(
        int id,
        string name, 
        double? price, 
        string? phoneNumber, 
        string? email, 
        string address, 
        int cityId)
    {
        return new Restaurant(id, name, price, phoneNumber, email, address, cityId);
    }
}