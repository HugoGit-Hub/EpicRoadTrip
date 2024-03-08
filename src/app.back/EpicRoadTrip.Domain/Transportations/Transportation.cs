﻿using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.ErrorHandling.Generics;
using EpicRoadTrip.Domain.Transportations.Exceptions;

namespace EpicRoadTrip.Domain.Transportations;

public sealed class Transportation
{
    public int Id { get; init; }

    public double Score { get; }

    public string Company { get; }

    public string Address { get; }

    public TransportationType TransportationType { get; }
    
    private Transportation(
        double score,
        string company,
        string address,
        TransportationType transportationType)
    {
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

        Score = score;
        Company = company;
        Address = address;
        TransportationType = transportationType;
    }

    public static Result<Transportation> Create(
        double score, 
        string company, 
        string address, 
        TransportationType transportationType)
    {
        try
        {
            var transportation = new Transportation(score, company, address, transportationType);

            return Result<Transportation>.Success(transportation);
        }
        catch (Exception e)
        {
            return Result<Transportation>.Failure(GenericErrors<Transportation>.InvalidFormatError(e));
        }
    }
}