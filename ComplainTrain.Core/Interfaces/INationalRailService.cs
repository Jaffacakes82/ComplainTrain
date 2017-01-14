using System.Collections.Generic;
using System.Threading.Tasks;
using ComplainTrain.Core.Classes;

namespace ComplainTrain.Core.Interfaces
{
    public interface INationalRailService
    {
        Task<IList<Departure>> GetDepartureBoard(int pageSize, string stationCode, string stationFilter);
    }
}
