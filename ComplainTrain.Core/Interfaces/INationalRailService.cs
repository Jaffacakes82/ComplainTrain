using System.Collections.Generic;
using System.Threading.Tasks;
using ComplainTrain.Core.Classes;

namespace ComplainTrain.Core.Interfaces
{
    public interface INationalRailService
    {
        IList<Departure> GetDepartureBoard(string pageSize, string stationCode, string stationFilter, string timeOffset, string timeWindow);
    }
}
