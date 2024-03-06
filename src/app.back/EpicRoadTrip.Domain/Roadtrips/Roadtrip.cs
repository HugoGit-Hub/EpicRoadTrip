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

    public static Roadtrip Create(
        double budget,
        DateTime startDate,
        DateTime? endDate,
        int userId)
    {
        return new Roadtrip(budget, startDate, endDate, userId);
    }
}