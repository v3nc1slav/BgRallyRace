namespace BgRallyRace.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using BgRallyRace.Models;
    using Microsoft.AspNetCore.Authorization;
    using BgRallyRace.Services;
    using Microsoft.AspNetCore.Http;
    using BgRallyRace.ViewModels;

    [Authorize]
    public class TeamsController : Controller
    {
        private readonly ILogger<TeamsController> _logger;
        private readonly IRallyPilotsServices pilot;
        private readonly IRallyNavigatorsServices navigator;
        private readonly ITeamServices team;


        public TeamsController(ILogger<TeamsController> logger, IRallyPilotsServices dbPilot,
            IRallyNavigatorsServices dbNavigator, ITeamServices teamServices)
        {
            _logger = logger;
            pilot = dbPilot;
            navigator = dbNavigator;
            this.team = teamServices;
        }

        [HttpGet]
        public async Task<IActionResult> Pilot(string input = null)
        {
            _logger.LogInformation("veiw pilot");
            var viewModel = new PilotViewModels
            {
                Pilots = pilot.GetPilots(User.Identity.Name),
                Text = input
            };
            if (!(viewModel.Pilots[0] == null))
            {
                TempData["Pilots"] = viewModel.Pilots.Select(x => x.Id).ToArray();
            }
            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Navigator(string input = null)
        {
            _logger.LogInformation("veiw navigator");
            var viewModel = new NavigatorViewModels
            {
                Navigators = navigator.GetNavigators(User.Identity.Name),
                Text = input
            };
            if (!(viewModel.Navigators[0] == null))
            {
                TempData["Navigators"] = viewModel.Navigators.Select(x => x.Id).ToArray();
            }
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam(string textTeam)
        {
            _logger.LogInformation("create team");
            team.CreateTeam(textTeam, User.Identity.Name);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> IncreaseSalarylPilot(int id)
        {
            _logger.LogInformation("increase salary pilot");
            pilot.IncreaseSalary(id, 100);
            return this.RedirectToAction("Pilot", "Teams");
        }

        [HttpGet]
        public async Task<IActionResult> IncreaseSalaryNavigator(int id)
        {
            _logger.LogInformation("increase salary pilot");
            navigator.IncreaseSalary(id, 100);
            return this.RedirectToAction("Navigator", "Teams");
        }

        [HttpGet]
        public async Task<IActionResult> IncreaseSalaryFitter()
        {
            _logger.LogInformation("increase salary fitter");
            return this.RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
