using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SignalRDemo2.Web.Models;

namespace SignalRDemo2.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<EndPointsSettingsModel> _config;

        public HomeController(IOptions<EndPointsSettingsModel> config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            return View(
                new EndPointsSettingsModel { FunctionURL = _config.Value.FunctionURL }
            );
        }
    }
}
