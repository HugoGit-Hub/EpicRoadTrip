﻿using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.ErrorHandling.Generics;
using EpicRoadTrip.Domain.Routes;
using EpicRoadTrip.Domain.Transportations.Exceptions;

namespace EpicRoadTrip.Domain.Transportations;

public sealed class Transportation
{
    public int Id { get; }

    public double Cost { get; }
    
    public double Score { get; }

    public string Company { get; }

    public string Address { get; }

    public TransportationType TransportationType { get; }

    public int RouteId { get; }

    public Route Route { get; } = null!;
    
    private Transportation(
        int id,
        double cost,
        double score,
        string company,
        string address,
        TransportationType transportationType,
        int routeId)
    {
        if (cost <= 0)
        {
            throw new TransportationInvalidCostException();
        }

        if (score is < 0.0 or > 5.0)
        {
            throw new TransportationInvalidScoreException();
        }

        if (string.IsNullOrWhiteSpace(company))
        {
            throw new TransportationInvalidCompanyException();
        }

        if (string.IsNullOrWhiteSpace(address))
        {
            throw new TransportationInvalidAddressException();
        }

        Id = id;
        Cost = cost;
        Score = score;
        Company = company;
        Address = address;
        TransportationType = transportationType;
        RouteId = routeId;
    }

    public static Result<Transportation> Create(
        int id,
        double cost,
        double score, 
        string company, 
        string address, 
        TransportationType transportationType,
        int routeId)
    {
        try
        {
            var transportation = new Transportation(id, cost, score, company, address, transportationType, routeId);

            return Result<Transportation>.Success(transportation);
        }
        catch (Exception e)
        {
            return Result<Transportation>.Failure(GenericErrors<Transportation>.InvalidFormatError(e));
        }
    }
}