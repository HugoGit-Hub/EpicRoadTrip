﻿using EpicRoadTrip.Domain.Institutions;

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

    public static Event Create(
        int id,
        string name,
        double? price,
        string? phoneNumber,
        string? email,
        string address, 
        int cityId)
    {
        return new Event(id, name, price, phoneNumber, email, address, cityId);
    }
}