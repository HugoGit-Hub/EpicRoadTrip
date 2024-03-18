using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicRoadTrip.Infrastructure.Externals.Train;

public class TrainJourneyStep
{
    public DateTime StartDate { get; set; }
    public DateTime ArrivalDate { get; set; }
    public string StartPlaceName { get; set; } = null!;
    public string ArrivalPlaceName { get; set; } = null!;
    public string Mode { get; set; } = null!;
    public int Duration { get; set; }
    public object Co2Emission { get; set; } = null!;
    public string GeoJson { get; set; } = null!;
}
