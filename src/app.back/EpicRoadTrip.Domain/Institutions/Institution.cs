﻿using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.ErrorHandling.Generics;
using EpicRoadTrip.Domain.Institutions.Exceptions;
using EpicRoadTrip.Domain.Roadtrips;

namespace EpicRoadTrip.Domain.Institutions;

public class Institution
{
    public int Id { get; }

    public string Name { get; }

    public double? Price { get; }

    public string? PhoneNumber { get; }

    public string? Email { get; }

    public string Address { get; }

    public InstitutionType Type { get; }

    public string? WebSite { get; }

    public Tuple<float, float> Coord { get; }


    public int RoadTripId { get; }
    public Roadtrip RoadTrip { get; } = null!;

    private Institution(
        int id,
        string name,
        double? price,
        string? phoneNumber,
        string? email,
        string address,
        InstitutionType type,
        int roadTripId,
        string? webSite,
        Tuple<float, float> coord
        )
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InstitutionInvalidNameException();
        }

        if (string.IsNullOrWhiteSpace(address))
        {
            throw new InstitutionInvalidAddressException();
        }
        
        Id = id;
        Name = name;
        Price = price;
        PhoneNumber = phoneNumber;
        Email = email;
        Address = address;
        Type = type;
        RoadTripId = roadTripId;
        WebSite = webSite;
        Coord = coord;
    }

    public static Result<Institution> Create(
        int id,
        string name,
        double? price,
        string? phoneNumber,
        string? email,
        string address,
        InstitutionType type,
        int roadTripId,
        string? webSite,
        Tuple<float, float> coord)
    {
        try
        {
            var institution = new Institution(id, name, price, phoneNumber, email, address, type, roadTripId, webSite, coord);

            return Result<Institution>.Success(institution);
        }
        catch (Exception e)
        {
            return Result<Institution>.Failure(GenericErrors<Institution>.InvalidFormatError(e));
        }
    }
}