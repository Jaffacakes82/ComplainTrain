using System.Threading.Tasks;
using TrainComplain.Core.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TrainComplain.Web.Models;

namespace TrainComplain.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            StationModel model = new StationModel();
            return this.View("Index", model);
        }
    }
}