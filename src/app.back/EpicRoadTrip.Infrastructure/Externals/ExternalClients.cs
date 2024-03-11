using EpicRoadTrip.Infrastructure.Externals.Configuration;

namespace EpicRoadTrip.Infrastructure.Externals;

public class ExternalClients
{
    public static HttpClient TrainClient = new()
    {
        BaseAddress = new Uri(DataSources.TRAIN_API_URL)
    };
}