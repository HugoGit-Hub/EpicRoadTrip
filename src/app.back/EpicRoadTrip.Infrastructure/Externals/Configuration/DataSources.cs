﻿namespace EpicRoadTrip.Infrastructure.Externals.Configuration;

public static class DataSources
{
    public static string TRAIN_API_URL = "https://c2abae8f-d08b-421c-ba9b-61fc0576ce96@api.navitia.io";
    public static string TRAIN_BASE_PATH = "/v1/coverage/sncf/";
    public static string CAR_API_URL = "https://wxs.ign.fr";
    public static string CAR_BASE_PATH = "/calcul/geoportail/itineraire/rest/1.0.0/";
    public static string PEDESTRIAN_API_URL = "https://wxs.ign.fr";
    public static string PEDESTRIAN_BASE_PATH = "/calcul/geoportail/itineraire/rest/1.0.0/";
    public static string BAR_API_URL = "https://discover.search.hereapi.com";
    public static string BAR_BASE_PATH = "/v1/";
    public static string HOTEL_API_URL = "https://booking-com.p.rapidapi.com";
    public static string HOTEL_BASE_PATH = "/v2/hotels/";
}
