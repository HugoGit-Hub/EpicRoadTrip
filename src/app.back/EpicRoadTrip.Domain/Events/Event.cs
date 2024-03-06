using EpicRoadTrip.Domain.Institutions;

namespace EpicRoadTrip.Domain.Events;

public sealed class Event : Institution
{
    public int Id { get; init; }

    private Event(
        string name,
        double? price,
        string? phoneNumber,
        string? email,
        string address,
        int cityId) 
        : base(name, price, phoneNumber, email, address, cityId)
    {
    }

    public static Event Create(
        string name,
        double? price,
        string? phoneNumber,
        string? email,
        string address, 
        int cityId)
    {
        return new Event(name, price, phoneNumber, email, address, cityId);
    }
}