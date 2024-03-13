﻿using EpicRoadTrip.Domain.Transportations;

namespace EpicRoadTrip.Application.Transportations.GetTransportation;

public record GetTransportationResponse
{
    public int Id { get; init; }

    public double Cost { get; init; }

    public double Score { get; init; }

    public required string Company { get; init; }

    public required string Address { get; init; }

    public TransportationType TransportationType { get; init; }

    public int RouteId { get; init; }
}