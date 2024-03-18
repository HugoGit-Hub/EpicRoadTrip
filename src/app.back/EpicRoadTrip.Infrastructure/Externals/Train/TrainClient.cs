using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Infrastructure.Externals.Configuration;
using Mapster;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EpicRoadTrip.Infrastructure.Externals.Train;

public class TrainClient : IExternalClient
{
    private readonly string _basePath = DataSources.TRAIN_BASE_PATH;

    public TrainClient()
    {
        Client = new HttpClient();
        Client.BaseAddress = new Uri(DataSources.TRAIN_API_URL);
        Client.DefaultRequestHeaders.Add("Authorization", "c2abae8f-d08b-421c-ba9b-61fc0576ce96");
    }

    private HttpClient Client;

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
                    return Result<T>.Failure(new Error("410", "Unable to parse data from train external source"));
                }
            }
            return Result<T>.Failure(new Error("410", "Unable to parse data from train external source"));
        }
        else
        {
            return Result<T>.Failure(new Error("410", "Unable to access data from train external source"));
        }
    }
}
