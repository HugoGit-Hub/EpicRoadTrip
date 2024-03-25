using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Institutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicRoadTrip.Infrastructure.Institutions;

public static class ExternalInstitutionHelper
{
    public static Result<Institution> CreateInstitutionFromJson(dynamic currentItem, InstitutionType givenType)
    {
        string name = (string)currentItem.title;
        double price = 0.0D;
        var phoneNumber = "";
        var email = "";
        string address = (string)currentItem.address.label.Value;
        InstitutionType type = givenType;
        int roadTripId = 0;
        var webSite = "";
        float lat_received = (float)currentItem.position.lat.Value;
        float lng_received = (float)currentItem.position.lng.Value;

        if (currentItem.contacts?[0]?.phone?[0]?.value.Value != null)
        {
            phoneNumber = currentItem.contacts[0]?.phone[0]?.value.Value;
        }

        if (currentItem.contacts?[0]?.www?[0]?.value.Value != null)
        {
            webSite = currentItem.contacts[0]?.www[0]?.value.Value;
        }

        if (currentItem.contacts?[0]?.email?[0]?.value.Value != null)
        {
            email = currentItem.contacts[0]?.email[0]?.value.Value;
        }

        return Institution.Create(
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
            null
            );
    }

    public static Dictionary<string, string> GetQueryString(float lat, float lng, int radius, string filter)
    {
        return new Dictionary<string, string>
        {
            { "in", "circle:" + lat.ToString().Replace(',', '.') + "," + lng.ToString().Replace(',', '.') + ";r=" + radius },
            { "q", filter },
            { "limit", "9" }
        };
    }
}
