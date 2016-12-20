using Microsoft.AspNetCore.Mvc;

namespace TrainComplain.Web.Models
{
    public class StationModel
    {   
        [HiddenInput]
        public virtual string SelectedStation { get; set; }
    }
}