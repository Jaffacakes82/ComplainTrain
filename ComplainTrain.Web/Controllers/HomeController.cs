using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComplainTrain.Web.Models;
using System.Collections.Generic;
using ComplainTrain.Core.Classes;
using System.Linq;
using ComplainTrain.Core.Interfaces;
using ComplainTrain.Core.Settings;
using Microsoft.Extensions.Options;

namespace ComplainTrain.Web.Controllers
{
    public sealed class HomeController : Controller
    {
        private readonly INationalRailService trainService;
        private readonly WebSettings options;

        public HomeController(INationalRailService trainService, IOptions<WebSettings> options)
        {
            this.trainService = trainService;
            this.options = options.Value;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return this.View("Index");
        }

        [HttpGet]
        public JsonResult SearchStations(string term)
        {
            IList<KeyValuePair<string, string>> matches = StationList.Stations.Where(
                station => station.Value.ToLowerInvariant().StartsWith(term.ToLowerInvariant())
                ).ToList();
            return this.Json(matches);
        }

        [HttpGet]
        public async Task<JsonResult> GetDepartures(string selectedStation)
        {
            DepartureListModel model = new DepartureListModel();
            IList<Departure> departures = await this.trainService.GetDepartureBoard("10", selectedStation, "to", "0", "60");
            model.DepartureModels = departures.Select(departure => new DepartureModel(departure)).ToList();
            return this.Json(model);
        }
    }
}