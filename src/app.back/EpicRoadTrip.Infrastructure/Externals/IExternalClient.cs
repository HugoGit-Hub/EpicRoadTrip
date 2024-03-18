using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Infrastructure.Externals.Configuration;
using System.Net.Http.Headers;

namespace EpicRoadTrip.Infrastructure.Externals;

public interface IExternalClient
{
    public Task<Result<T>> GetData<T>(string command, Dictionary<string, string> queryParams) where T : class;
}