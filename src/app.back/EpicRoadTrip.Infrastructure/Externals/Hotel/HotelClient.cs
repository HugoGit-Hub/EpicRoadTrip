using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Externals;
using EpicRoadTrip.Infrastructure.Externals.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicRoadTrip.Infrastructure.Externals.Hotel;

public class HotelClient : IExternalClient
{
    private readonly string _basePath = DataSources.HOTEL_BASE_PATH;
    private HttpClient Client;

    public HotelClient(HttpClient client)
    {
        Client = client;
        Client.BaseAddress = new Uri(DataSources.HOTEL_API_URL);
        Client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "6516e309b9mshdeaa4e94c24155ap1ade1ejsn1a64b4fcbd2c");
        Client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "booking-com.p.rapidapi.com");
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
                    return Result<T>.Failure(new Error("410", "Unable to parse data from car external source"));
                }
            }
            return Result<T>.Failure(new Error("410", "Unable to parse data from car external source"));
        }
        else
        {
            return Result<T>.Failure(new Error("410", "Unable to access data from car external source"));
        }
    }
}
