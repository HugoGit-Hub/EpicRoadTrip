using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Externals;
using EpicRoadTrip.Infrastructure.Externals.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicRoadTrip.Infrastructure.Externals.Pedestrian;

public class PedestrianClient : IExternalClient
{
    private readonly string _basePath = DataSources.PEDESTRIAN_BASE_PATH;
    private HttpClient Client;

    public PedestrianClient(HttpClient client)
    {
        Client = client;
        Client.BaseAddress = new Uri(DataSources.PEDESTRIAN_API_URL);
    }

    public async Task<Result<T>> GetData<T>(string command, Dictionary<string, string> queryParams) where T : class
    {
        var formattedQueryParams = _basePath + command + "?";

        foreach (var param in queryParams)
        {
            formattedQueryParams += param.Key + "=" + param.Value + "&";
        }

        var resp = Client.GetAsync(formattedQueryParams).Result;

        if (resp.IsSuccessStatusCode)
        {
            var result = await resp.Content.ReadAsStringAsync();

            if (result != null)
            {
                dynamic? results = JsonConvert.DeserializeObject<dynamic>(result);

                if (results != null)
                {
                    return Result<T>.Success(results);
                }
                else
                {
                    return Result<T>.Failure(new Error("410", "Unable to parse data from pedestrian external source"));
                }
            }
            return Result<T>.Failure(new Error("410", "Unable to parse data from pedestrian external source"));
        }
        else
        {
            return Result<T>.Failure(new Error("410", "Unable to access data from pedestrian external source"));
        }
    }
}
