using EpicRoadTrip.Domain.ErrorHandling;

namespace EpicRoadTrip.Domain.External;

public interface IExternalClientGet
{
    public Task<Result<T>> GetData<T>(string command, Dictionary<string, string> queryParams) where T : class;
}