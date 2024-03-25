using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.External;
using EpicRoadTrip.Domain.Institutions;
using EpicRoadTrip.Domain.Roadtrips;
using EpicRoadTrip.Domain.Routes;
using EpicRoadTrip.Domain.Transportations;
using EpicRoadTrip.Infrastructure.Externals.Bar;
using EpicRoadTrip.Infrastructure.Externals.Car;
using EpicRoadTrip.Infrastructure.Externals.Hotel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EpicRoadTrip.Infrastructure.Institutions;

public class ExternalInstitutionService(InstitutionClient institutionClient, HotelClient hotelClient) : IExternalInstitutionService
{
    public async Task<Result<IEnumerable<Institution>>> GetBarAround(float lat, float lng, int radius, CancellationToken cancellationToken)
    {
        var formattedParams = ExternalInstitutionHelper.GetQueryString(lat, lng, radius, "bar");

        Result<dynamic> dResult = await institutionClient.GetData<dynamic>("discover", formattedParams);


        if (dResult.IsSuccess)
        {
            dynamic dJson = dResult.Value;
            var institutionResult = new List<Institution>();

            foreach (var currentItem in dJson.items)
            {
                Result<Institution> correspondingInst = ExternalInstitutionHelper.CreateInstitutionFromJson(currentItem, InstitutionType.Bar);

                if (correspondingInst.IsSuccess)
                {
                    institutionResult.Add(correspondingInst.Value);
                }
            }

            return Result<IEnumerable<Institution>>.Success(institutionResult);
        }
        else
        {
            return Result<IEnumerable<Institution>>.Failure(new Error("", ""));
        }
    }

    public async Task<Result<IEnumerable<Institution>>> GetEventAround(float lat, float lng, int radius, CancellationToken cancellationToken)
    {
        var formattedParams = ExternalInstitutionHelper.GetQueryString(lat, lng, radius, "loisir");

        Result<dynamic> dResult = await institutionClient.GetData<dynamic>("discover", formattedParams);


        if (dResult.IsSuccess)
        {
            dynamic dJson = dResult.Value;
            var institutionResult = new List<Institution>();

            foreach (var currentItem in dJson.items)
            {
                Result<Institution> correspondingInst = ExternalInstitutionHelper.CreateInstitutionFromJson(currentItem, InstitutionType.Event);

                if (correspondingInst.IsSuccess)
                {
                    institutionResult.Add(correspondingInst.Value);
                }
            }

            return Result<IEnumerable<Institution>>.Success(institutionResult);
        }
        else
        {
            return Result<IEnumerable<Institution>>.Failure(new Error("", ""));
        }
    }

    public async Task<Result<IEnumerable<Institution>>> GetHotelAround(float lat, float lng, int radius, DateOnly? checkin, DateOnly? checkout, CancellationToken cancellationToken)
    {
        if (checkin == null || checkout == null)
        {
            return Result<IEnumerable<Institution>>.Failure(new Error("455", "To get hotel preview you must set checkin and checkout date"));
        }
        else
        {
            DateTime checkinDt = new DateTime();
            DateTime checkoutDt = new DateTime();

            if (checkin != null)
            {
                checkinDt = checkin.Value.ToDateTime(new TimeOnly(0, 0, 0));
            }

            if (checkout != null)
            {
                checkoutDt = checkout.Value.ToDateTime(new TimeOnly(0, 0, 0));
            }

            var formattedParams = new Dictionary<string, string>
            {
                { "checkin_date", checkinDt.ToString("yyyy-M-dd") },
                { "checkout_date", checkoutDt.ToString("yyyy-M-dd") },
                { "room_number", "1" },
                { "adults_number", "2" },
                { "units", "metric" },
                { "filter_by_currency", "EUR" },
                { "order_by", "popularity" },
                { "latitude", lat.ToString() },
                { "longitude", lng.ToString() },
                { "locale", "fr" }
            };

            Result<dynamic> dResult = await hotelClient.GetData<dynamic>("search-by-coordinates", formattedParams);


            if (dResult.IsSuccess)
            {
                dynamic dJson = dResult.Value;
                var institutionResult = new List<Institution>();

                foreach (var currentItem in dJson.results)
                {
                    string name = (string)currentItem.name;
                    double price = 0.0D;
                    var phoneNumber = "";
                    var email = "";
                    string address = "No adress";
                    InstitutionType type = InstitutionType.Hotel;
                    int roadTripId = 0;
                    var webSite = "";
                    float lat_received = (float)currentItem.latitude.Value;
                    float lng_received = (float)currentItem.longitude.Value;
                    string? previewUrl = (string)currentItem.photoMainUrl != null ? currentItem.photoMainUrl : null;

                    if (currentItem.priceBreakdown?.grossPrice?.value?.Value != null)
                    {
                        price = (double)currentItem.priceBreakdown.grossPrice.value.Value;
                    }

                    Result<Institution> correspondingInst = Institution.Create(
                        0,
                        name,
                        price,
                        phoneNumber,
                        email,
                        address,
                        type,
                        roadTripId,
                        webSite,
                        lat_received,
                        lng_received,
                        previewUrl
                        );


                    if (correspondingInst.IsSuccess)
                    {
                        institutionResult.Add(correspondingInst.Value);
                    }
                }

                return Result<IEnumerable<Institution>>.Success(institutionResult);
            }
            else
            {
                return Result<IEnumerable<Institution>>.Failure(new Error("", ""));
            }
        }
    }

    public async Task<Result<IEnumerable<Institution>>> GetRestaurantAround(float lat, float lng, int radius, CancellationToken cancellationToken)
    {
        var formattedParams = ExternalInstitutionHelper.GetQueryString(lat, lng, radius, "restaurant");

        Result<dynamic> dResult = await institutionClient.GetData<dynamic>("discover", formattedParams);


        if (dResult.IsSuccess)
        {
            dynamic dJson = dResult.Value;
            var institutionResult = new List<Institution>();

            foreach (var currentItem in dJson.items)
            {
                Result<Institution> correspondingInst = ExternalInstitutionHelper.CreateInstitutionFromJson(currentItem, InstitutionType.Restaurant);

                if (correspondingInst.IsSuccess)
                {
                    institutionResult.Add(correspondingInst.Value);
                }
            }

            return Result<IEnumerable<Institution>>.Success(institutionResult);
        }
        else
        {
            return Result<IEnumerable<Institution>>.Failure(new Error("", ""));
        }
    }
}
