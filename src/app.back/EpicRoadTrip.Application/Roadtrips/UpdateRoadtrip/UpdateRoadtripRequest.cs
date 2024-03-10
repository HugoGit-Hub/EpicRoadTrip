﻿namespace EpicRoadTrip.Application.Roadtrips.UpdateRoadtrip;

public record UpdateRoadtripRequest
{
    public int Id { get; init; }

    public double Budget { get; init; }

    public DateTime StartDate { get; init; }

    public DateTime? EndDate { get; init; }
}