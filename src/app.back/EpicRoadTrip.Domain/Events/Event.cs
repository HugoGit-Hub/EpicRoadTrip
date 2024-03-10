using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.ErrorHandling.Generics;
using EpicRoadTrip.Domain.Institutions;

namespace EpicRoadTrip.Domain.Events;

public sealed class Event : Institution
{
    public int Id { get; }

    private Event(
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

    public static Result<Event> Create(
        int id,
        string name,
        double? price,
        string? phoneNumber,
        string? email,
        string address, 
        int cityId)
    {
        try
        {
            var events = new Event(id, name, price, phoneNumber, email, address, cityId);

            return Result<Event>.Success(events);
        }
        catch (Exception e)
        {
            return Result<Event>.Failure(GenericErrors<Event>.InvalidFormatError(e));
        }
    }
}