using System.Security.Principal;
using Gaby.World.DAL.Data;
using GabyWorld.IOC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GabyWorld.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, 
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
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

        [Route("create")]
        public async Task<IActionResult> Create()
        {
            var result = await _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "DavidM",
                Email = "davidm@gmail.com"
            }, "password");

            if(result.Succeeded) 
                return Content("User was created", "text/html");

            return Content("User creation failed", "text/html");
        }

        [Authorize(AuthenticationSchemes = "Identity.Application")]//cookies authorization
        [Route("private")]
        public IActionResult Private()
        {
            return Content($"Welcome to the private area {HttpContext.User.Identity.Name}");
        }

        [Route("login")]
        public async Task<IActionResult> LogInAsync(string returnUrl)
        {
            //sign out previous session
            await _signInManager.SignOutAsync();

            // This creates the authentication cookie
            //Sets it on the response via the authentication middleware.
            var result = await _signInManager.PasswordSignInAsync("DavidM", "password", true, false);

            if(result.Succeeded)
            {
                //if we 
                if (string.IsNullOrEmpty(returnUrl))
                    return RedirectToAction(nameof(Index));
                
                return Redirect(returnUrl);
            }

            return Content("Failed to log in", "text/html");

        }

        [Route("logout")]
        public async Task<IActionResult> LogOutAsync()
        {
            await _signInManager.SignOutAsync();

            return Content("done");
        }
    }
}
