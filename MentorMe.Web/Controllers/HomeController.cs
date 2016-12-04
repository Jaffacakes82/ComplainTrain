using MentorMe.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MentorMe.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfigService configService;

        public HomeController(IConfigService configService)
        {
            this.configService = configService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Index(string foo)
        {
            this.configService.Get(foo);
            return this.RedirectToAction("Index");
        }
    }
}