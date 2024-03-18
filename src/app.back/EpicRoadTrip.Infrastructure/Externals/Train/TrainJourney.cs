using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicRoadTrip.Infrastructure.Externals.Train;

public class TrainJourney
{
    public DateTime StartDate {  get; set; }
    public DateTime ArrivalDate {  get; set; }
    public int Duration { get; set; }
    public int NbTransfers {  get; set; }
    public IEnumerable<string> Tags { get; set; } = null!;
    public object Co2Emission {  get; set; } = null!;
    public IEnumerable<TrainJourneyStep> Steps {  get; set; } = null!;
}
