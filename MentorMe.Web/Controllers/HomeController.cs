using System.Threading.Tasks;
using MentorMe.Core.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MentorMe.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly MessageSettings messageOptions;

        public HomeController(IOptions<MessageSettings> messageOptions)
        {
            this.messageOptions = messageOptions.Value;
        }

        [HttpGet]
        public ActionResult Index()
        {
            this.ViewData["Message"] = this.messageOptions.MessageOfTheDay;
            return this.View("Index");
        }
    }
}