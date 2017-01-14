using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComplainTrain.Web.Models;
using System.Collections.Generic;
using ComplainTrain.Core.Classes;
using System.Linq;

namespace ComplainTrain.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            StationModel model = new StationModel();
            return this.View("Index", model);
        }

        [HttpGet]
        public JsonResult SearchStations(string term)
        {
            IList<KeyValuePair<string, string>> matches = StationList.Stations.Where(station => station.Value.ToLowerInvariant().StartsWith(term.ToLowerInvariant())).ToList();
            return this.Json(matches);
        }
    }
}