using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.ErrorHandling.Generics;
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

    public static Result<Hotel> Create(
        int id,
        string name,
        double price,
        string phoneNumber,
        string email,
        string address,
        int cityId)
    {
        try
        {
            var hotel = new Hotel(id, name, price, phoneNumber, email, address, cityId);

            return Result<Hotel>.Success(hotel);
        }
        catch (Exception e)
        {
            return Result<Hotel>.Failure(GenericErrors<Hotel>.InvalidFormatError(e));
        }
    }
}