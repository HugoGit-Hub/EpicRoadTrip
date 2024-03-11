using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.ErrorHandling.Generics;
using EpicRoadTrip.Domain.Institutions;

namespace EpicRoadTrip.Domain.Bars;

public sealed class Bar : Institution
{
    public int Id { get; }

    private Bar(
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

    public static Result<Bar> Create(
        int id,
        string name,
        double? price,
        string? phoneNumber,
        string? email,
        string address, 
        int cityId)
    {
        try
        {
            var bar = new Bar(id, name, price, phoneNumber, email, address, cityId);

            return Result<Bar>.Success(bar);
        }
        catch (Exception e)
        {
            return Result<Bar>.Failure(GenericErrors<Bar>.InvalidFormatError(e));
        }
    }
}