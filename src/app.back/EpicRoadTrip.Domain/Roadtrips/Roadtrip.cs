using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.ErrorHandling.Generics;
using EpicRoadTrip.Domain.Roadtrips.Exceptions;
using EpicRoadTrip.Domain.Routes;
using EpicRoadTrip.Domain.Users;

namespace EpicRoadTrip.Domain.Roadtrips;

public sealed class Roadtrip
{
    public int Id { get; }

    public double Budget { get; }

    public DateTime StartDate { get; }

    public DateTime? EndDate { get; }
    public TimeSpan Duration { get; }
    public int NbTransfers { get; }
    public IEnumerable<string>? Tags { get; }
    public string? Co2Emission { get; }

    public int UserId { get; }

    public User User { get; } = null!;

    public ICollection<Route> Routes { get; } = [];

    private Roadtrip(
        int id,
        double budget,
        DateTime startDate,
        DateTime? endDate,
        int userId,
        TimeSpan duration,
        int nbTransfers,
        IEnumerable<string>? tags, 
        string? co2Emission
        )
    {
        if (budget <= 0)
        {
            throw new RoadtripNotValidBudgetException();
        }

        if (startDate > endDate)
        {
            throw new RoadtripNotValidDatesException();
        }

        Id = id;
        StartDate = startDate;
        EndDate = endDate;
        UserId = userId;
        Budget = budget;
        Duration = duration;
        NbTransfers = nbTransfers;
        Tags = tags;
        Co2Emission = co2Emission;
    }

    public static Result<Roadtrip> Create(
        int id,
        double budget,
        DateTime startDate,
        DateTime? endDate,
        int userId,
        TimeSpan duration,
        int nbTransfers,
        IEnumerable<string>? tags,
        string? co2Emission)
    {
        try
        {
            var roadtrip = new Roadtrip(id, budget, startDate, endDate, userId, duration, nbTransfers, tags, co2Emission);

            return Result<Roadtrip>.Success(roadtrip);
        }
        catch (Exception e)
        {
            return Result<Roadtrip>.Failure(GenericErrors<Roadtrip>.InvalidFormatError(e));
        }
    }
}