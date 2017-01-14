using Microsoft.AspNetCore.Mvc;

namespace ComplainTrain.Web.Models
{
    public class StationModel
    {   
        [HiddenInput]
        public virtual string SelectedStation { get; set; }
    }
}