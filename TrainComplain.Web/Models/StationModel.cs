using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainComplain.Core.Classes;

namespace TrainComplain.Web.Models
{
    public class StationModel
    {
        public StationModel()
        {
            this.Stations = new SelectList(StationList.Stations.OrderBy(station => station.Value), "Key", "Value");
        }

        public virtual SelectList Stations { get; }
        
        public virtual string SelectedStation { get; set; }
    }
}