using EpicRoadTrip.Domain.ErrorHandling;

namespace EpicRoadTrip.Domain.External;

public interface IExternalClientPost
{
    public Task<Result<TResponse>> PostData<TResponse, TParameters>(TParameters parameters, CancellationToken cancellationToken)
        where TResponse : class
        where TParameters : class;
}