﻿using EpicRoadTrip.Domain.Institutions;

namespace EpicRoadTrip.Application.Institutions.GetInstitution;

public record GetInstitutionResponse
{
    public int Id { get; init; }

    public required string Name { get; init; }

    public double? Price { get; init; }

    public string? PhoneNumber { get; init; }

    public string? Email { get; init; }

    public string? WebSite{ get; init; }

    public required string Address { get; init; }

    public required float Lat { get; init; }
    public required float Lng { get; init; }

    public InstitutionType Type { get; init; }

}