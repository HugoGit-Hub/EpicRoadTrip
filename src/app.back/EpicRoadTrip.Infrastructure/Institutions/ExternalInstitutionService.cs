using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.External;
using EpicRoadTrip.Domain.Institutions;
using EpicRoadTrip.Domain.Roadtrips;
using EpicRoadTrip.Domain.Routes;
using EpicRoadTrip.Domain.Transportations;
using EpicRoadTrip.Infrastructure.Externals.Bar;
using EpicRoadTrip.Infrastructure.Externals.Car;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EpicRoadTrip.Infrastructure.Institutions;

public class ExternalInstitutionService(BarClient barClient) : IExternalInstitutionService
{
    public async Task<Result<IEnumerable<Institution>>> GetBarAround(Tuple<float, float> placeCoord, int radius, CancellationToken cancellationToken)
    {
        var formattedParams = new Dictionary<string, string>
        {
            { "in", "circle:" + placeCoord.Item1.ToString().Replace(',', '.') + "," + placeCoord.Item2.ToString().Replace(',', '.') + ";r=" + radius },
            { "q", "bar" },
            { "limit", "9" }
        };

        Result<dynamic> dResult = await barClient.GetData<dynamic>("discover", formattedParams);


        if (dResult.IsSuccess)
        {
            dynamic dJson = dResult.Value;
            var institutionResult = new List<Institution>();

            foreach (var currentItem in dJson.items)
            {

                var name = currentItem.title;
                var price = 0;
                var phoneNumber = "";
                var email = "";
                var address = currentItem.address.label.Value;
                InstitutionType type = InstitutionType.Bar;
                var roadTripId = 0;
                var webSite = "";
                var coord = new Tuple<float, float>(currentItem.position.lat.Value, currentItem.position.lng.Value);

                if (currentItem.contacts[0]?.phone[0]?.value.Value != null)
                {
                    phoneNumber = currentItem.contacts[0]?.phone[0]?.value.Value;
                }

                if (currentItem.contacts[0]?.www[0]?.value.Value != null)
                {
                    webSite = currentItem.contacts[0]?.www[0]?.value.Value;
                }

                if (currentItem.contacts[0]?.email[0]?.value.Value != null)
                {
                    email = currentItem.contacts[0]?.email[0]?.value.Value;
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
                    coord
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

    public Task<Result<IEnumerable<Institution>>> GetEventAround(Tuple<float, float> placeCoord, int radius, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<Institution>>> GetHotelAround(Tuple<float, float> placeCoord, int radius, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<Institution>>> GetRestaurantAround(Tuple<float, float> placeCoord, int radius, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
