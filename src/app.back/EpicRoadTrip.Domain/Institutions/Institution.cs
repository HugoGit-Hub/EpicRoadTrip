using EpicRoadTrip.Domain.Cities;
using EpicRoadTrip.Domain.Institutions.Exceptions;

namespace EpicRoadTrip.Domain.Institutions;

public class Institution
{
    public string Name { get; }

    public double? Price { get; }

    public string? PhoneNumber { get; }

    public string? Email { get; }

    public string Address { get; }

    public int CityId { get; }

    public City City { get; } = null!;

    protected Institution(
        string name,
        double? price,
        string? phoneNumber,
        string? email,
        string address,
        int cityId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InstitutionInvalidNameException();
        }

        if (string.IsNullOrWhiteSpace(address))
        {
            throw new InstitutionInvalidAddressException();
        }
        
        Name = name;
        Price = price;
        PhoneNumber = phoneNumber;
        Email = email;
        Address = address;
        CityId = cityId;
    }
}