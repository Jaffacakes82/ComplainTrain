using System.Collections.Generic;
using System.Threading.Tasks;
using ComplainTrain.Core.Classes;
using System;

namespace ComplainTrain.Core.Interfaces
{
    public class NationalRailService : INationalRailService
    {
        public async Task<IList<Departure>> GetDepartureBoard(int pageSize, string stationCode, string stationFilter)
        {
            throw new NotImplementedException();
        }
    }
}
