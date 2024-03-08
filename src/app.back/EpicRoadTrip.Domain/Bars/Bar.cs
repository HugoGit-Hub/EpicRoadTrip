using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.ErrorHandling.Generics;
using EpicRoadTrip.Domain.Institutions;

namespace EpicRoadTrip.Domain.Bars;

public sealed class Bar : Institution
{
    public int Id { get; init; }

    private Bar(
        string name,
        double? price,
        string? phoneNumber,
        string? email,
        string address,
        int cityId) 
        : base(name, price, phoneNumber, email, address, cityId)
    {
    }

    public static Result<Bar> Create(
        string name,
        double? price,
        string? phoneNumber,
        string? email,
        string address, 
        int cityId)
    {
        try
        {
            var bar = new Bar(name, price, phoneNumber, email, address, cityId);

            return Result<Bar>.Success(bar);
        }
        catch (Exception e)
        {
            return Result<Bar>.Failure(GenericErrors<Bar>.InvalidFormatError(e));
        }
    }
}