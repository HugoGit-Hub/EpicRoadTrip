using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.External;
using EpicRoadTrip.Infrastructure.Externals.Configuration;
using Newtonsoft.Json;

namespace EpicRoadTrip.Infrastructure.Externals.Train;

public class TrainClientGet : IExternalClientGet
{
    private readonly string _basePath = DataSources.TRAIN_BASE_PATH;
    private HttpClient Client;

    public TrainClientGet(HttpClient client)
    {
        Client = client;
        Client.BaseAddress = new Uri(DataSources.TRAIN_API_URL);
        Client.DefaultRequestHeaders.Add("Authorization", "c2abae8f-d08b-421c-ba9b-61fc0576ce96");
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
