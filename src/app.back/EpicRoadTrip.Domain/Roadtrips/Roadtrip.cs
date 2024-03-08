using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.ErrorHandling.Generics;
using EpicRoadTrip.Domain.Roadtrips.Exceptions;
using EpicRoadTrip.Domain.Routes;
using EpicRoadTrip.Domain.Users;

namespace EpicRoadTrip.Domain.Roadtrips;

public sealed class Roadtrip
{
    public int Id { get; init; }

    public double Budget { get; }

    public DateTime StartDate { get; }

    public DateTime? EndDate { get; }

    public int UserId { get; }

    public User User { get; } = null!;

    public ICollection<Route> Routes { get; } = [];

    private Roadtrip(
        double budget,
        DateTime startDate,
        DateTime? endDate,
        int userId)
    {
        if (budget <= 0)
        {
            throw new RoadtripNotValidBudgetException();
        }

        if (startDate > endDate)
        {
            throw new RoadtripNotValidDatesException();
        }

        StartDate = startDate;
        EndDate = endDate;
        UserId = userId;
        Budget = budget;
    }

    public static Result<Roadtrip> Create(
        double budget,
        DateTime startDate,
        DateTime? endDate,
        int userId)
    {
        try
        {
            var roadtrip = new Roadtrip(budget, startDate, endDate, userId);

            return Result<Roadtrip>.Success(roadtrip);
        }
        catch (Exception e)
        {
            return Result<Roadtrip>.Failure(GenericErrors<Roadtrip>.InvalidFormatError(e));
        }
    }
}