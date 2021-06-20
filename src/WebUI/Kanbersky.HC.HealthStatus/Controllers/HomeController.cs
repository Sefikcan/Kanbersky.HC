using Microsoft.AspNetCore.Mvc;

namespace Kanbersky.HC.HealthStatus.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/healthchecks-ui");
        }
    }
}
