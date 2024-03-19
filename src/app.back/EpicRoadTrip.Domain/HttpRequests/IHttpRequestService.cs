using EpicRoadTrip.Domain.ErrorHandling;

namespace EpicRoadTrip.Domain.HttpRequests;

public interface IHttpRequestService<TResponse, in TParameter> 
    where TResponse : class
    where TParameter : class
{
    public Task<Result<TResponse>> PostData(TParameter parameters, CancellationToken cancellationToken);

    public Task<Result<TResponse>> GetData(string url, TParameter query, CancellationToken cancellationToken);
}