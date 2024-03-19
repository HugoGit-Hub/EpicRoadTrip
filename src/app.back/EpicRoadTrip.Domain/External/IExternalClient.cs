using EpicRoadTrip.Domain.ErrorHandling;

namespace EpicRoadTrip.Domain.Externals;

public interface IExternalClient
{
    public Task<Result<T>> GetData<T>(string command, Dictionary<string, string> queryParams) where T : class;
}