namespace BgRallyRace.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using BgRallyRace.Models;
    using Microsoft.AspNetCore.Authorization;
    using BgRallyRace.Data;
    using BgRallyRace.Services;
    using Microsoft.AspNetCore.Http;
    using BgRallyRace.ViewModels;
    public class TeamsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext db { get; set; } = new ApplicationDbContext();
        private IRallyPilotsServices pilot { get; set; }
        private IRallyNavigatorsServices navigator { get; set; }

        public TeamsController(ILogger<HomeController> logger, IRallyPilotsServices dbPilot,
            IRallyNavigatorsServices dbNavigator)
        {
            _logger = logger;
            pilot = dbPilot;
            navigator = dbNavigator;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Pilot()
        {
            var viewModel = new PilotViewModels
            {
                Pilots = pilot.GetPilots(User.Identity.Name)
            };
            if (!(viewModel.Pilots[0] == null))
            {
                TempData["Pilots"] = viewModel.Pilots.Select(x => x.Id).ToArray();
            }
            return this.View(viewModel);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Navigator()
        {
            var viewModel = new NavigatorViewModels
            {
                Navigators = navigator.GetNavigators(User.Identity.Name)
            };
            if (!(viewModel.Navigators[0] == null))
            {
                TempData["Navigators"] = viewModel.Navigators.Select(x => x.Id).ToArray();
            }
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateTeam(string textTeam)
        {
            var team = new TeamServices(db);
            team.CreateTeam(textTeam, User.Identity.Name);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public IActionResult IncreaseSalarylPilot(int id)
        {
            pilot.IncreaseSalary(id, 100);
            return this.RedirectToAction("Pilot", "Teams");
        }

        [Authorize]
        [HttpGet]
        public IActionResult IncreaseSalaryNavigator(int id)
        {
            navigator.IncreaseSalary(id, 100);
            return this.RedirectToAction("Navigator", "Teams");
        }

        [Authorize]
        public IActionResult IncreaseSalaryFitter()
        {
            return this.RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
      
    }
}
