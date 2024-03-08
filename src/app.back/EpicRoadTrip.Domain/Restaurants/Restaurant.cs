using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.ErrorHandling.Generics;
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

    public static Result<Restaurant> Create(
        string name, 
        double? price, 
        string? phoneNumber, 
        string? email, 
        string address, 
        int cityId)
    {
        try
        {
            var restaurant = new Restaurant(name, price, phoneNumber, email, address, cityId);

            return Result<Restaurant>.Success(restaurant);
        }
        catch (Exception e)
        {
            return Result<Restaurant>.Failure(GenericErrors<Restaurant>.InvalidFormatError(e));
        }
    }
}