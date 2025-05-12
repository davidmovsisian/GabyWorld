using Microsoft.AspNetCore.Mvc;

namespace GabyWorld.Controllers
{
    [Route("about")]
    public class AboutController : Controller
    {
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("tellmemore/{more?}")]
        public IActionResult TellMeMore(string more)
        {
            //return new JsonResult(new { action = "tellmemore", param = more });
            return View();
        }
    }
}
