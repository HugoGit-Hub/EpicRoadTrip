using EpicRoadTrip.Domain.Bars;
using EpicRoadTrip.Domain.Cities.Exceptions;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.ErrorHandling.Generics;
using EpicRoadTrip.Domain.Events;
using EpicRoadTrip.Domain.Hotels;
using EpicRoadTrip.Domain.Restaurants;
using EpicRoadTrip.Domain.Routes;

namespace EpicRoadTrip.Domain.Cities;

public sealed class City
{
    public int Id { get; }

    public string Name { get; }
 
    public ICollection<Route> Routes { get; } = [];

    public ICollection<Hotel> Hotels { get; } = [];

    public ICollection<Bar> Bars { get; } = [];

    public ICollection<Restaurant> Restaurants { get; } = [];
    
    public ICollection<Event> Events { get; } = [];

    private City(int id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new CityInvalidNameException();
        }

        Id = id;
        Name = name;
    }

    public static Result<City> Create(int id, string name)
    {
        try
        {
            var city = new City(id, name);

            return Result<City>.Success(city);
        }
        catch (Exception e)
        {
            return Result<City>.Failure(GenericErrors<City>.InvalidFormatError(e));
        }
    }
}