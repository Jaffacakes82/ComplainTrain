using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComplainTrain.Web.Models;
using System.Collections.Generic;
using ComplainTrain.Core.Classes;
using System.Linq;
using ComplainTrain.Core.Interfaces;
using ComplainTrain.Core.Settings;
using Microsoft.Extensions.Options;
using ComplainTrain.Core.Helpers;
using Microsoft.AspNetCore.Hosting.Internal;

namespace ComplainTrain.Web.Controllers
{
    public sealed class HomeController : Controller
    {
        private readonly IList<string> hashtags = new List<string> { " #delays", " #crapservice", " #serviceoverprofit", " #selfish"};
        private readonly INationalRailService trainService;
        private readonly WebSettings options;
        private readonly ITwitterService twitterService;
        public HomeController(INationalRailService trainService, IOptions<WebSettings> options, ITwitterService twitterService)
        {
            this.twitterService = twitterService;   
            this.trainService = trainService;
            this.options = options.Value;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return this.View("Index");
        }

        [HttpGet]
        public ActionResult About()
        {
            return this.View("About");
        }

        [HttpGet]
        public JsonResult SearchStations(string term)
        {
            IList<KeyValuePair<string, string>> matches = StationList.Stations.Where(
                station => station.Value.ToLowerInvariant().Contains(term.ToLowerInvariant())
                ).ToList();

            return this.Json(matches);
        }

        [HttpGet]
        public JsonResult GetDepartures(string selectedStation)
        {
            DepartureListModel model = new DepartureListModel();
            IList<Departure> departures = this.trainService.GetDepartureBoard("10", selectedStation, "to", "0", "60");

            if (departures.Count > 0)
            {
                model.DepartureModels = departures.Select(departure => new DepartureModel(departure)).ToList();
            }

            return this.Json(model);
        }

        [HttpPost]
        public JsonResult Complain(ComplaintModel model)
        {
            var twitterHandle = TwitterLookup.TwitterHandles.FirstOrDefault(tweet => tweet.Key == model.Operator).Value;
            var delayedOrCancelled = model.Expected == "Cancelled" ? "CANCELLED" : "DELAYED";
            string message = string.Format(
                "#complaint received for {0}. The {1} from {2} to {3} is {4}. SORT IT OUT! @transportgovuk",
                twitterHandle,
                model.Due,
                model.OriginalSearch,
                model.Destination,
                delayedOrCancelled
            );

            while (message.Length < 140) 
            {
                int availableSpace = 140 - message.Length;
                string selectedHashtag = hashtags.Where(s => s.Length <= availableSpace && !message.Contains(s)).FirstOrDefault();

                if (string.IsNullOrWhiteSpace(selectedHashtag))
                {
                    break;
                }

                message += selectedHashtag;
            }
            
            this.twitterService.Tweet(message);
            return this.Json(model.Operator);
        }
    }
}