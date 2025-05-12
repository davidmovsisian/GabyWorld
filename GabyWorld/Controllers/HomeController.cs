using GabyWorld.Data;
using GabyWorld.IOC;
using Microsoft.AspNetCore.Mvc;

namespace GabyWorld.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            _context.Database.EnsureCreated();

            if (!_context.Settings.Any())
                _context.Settings.Add(new SettingsDataModel { Name = "Color", Value = "Red" });

            var localSettings = _context.Settings.Local.Count;
            var dbSettings = _context.Settings.Count();

            _context.SaveChanges();

            localSettings = _context.Settings.Local.Count;
            dbSettings = _context.Settings.Count();

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
