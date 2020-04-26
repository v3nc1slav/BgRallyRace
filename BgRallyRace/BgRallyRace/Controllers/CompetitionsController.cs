namespace BgRallyRace.Controllers
{
    using BgRallyRace.Models;
    using BgRallyRace.Services;
    using BgRallyRace.Services.Competitions;
    using BgRallyRace.Services.Runways;
    using BgRallyRace.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Diagnostics;
    using System.Threading.Tasks;

    [Authorize]
    public class CompetitionsController : Controller
    {
        private readonly ILogger<CompetitionsController> _logger;
        private readonly ICarServices cars;
        private readonly ICompetitionsServices competitions;
        private readonly ITeamServices teams;
        private readonly IRallyPilotsServices pilots;
        private readonly IRallyNavigatorsServices navigators;
        private readonly IRunwaysServices runways;

        public CompetitionsController(ILogger<CompetitionsController> logger,ICarServices carServices, ICompetitionsServices competitionsServices, 
            ITeamServices teamServices, IRallyPilotsServices rallyPilots, IRallyNavigatorsServices rallyNavigators, IRunwaysServices runwaysServices)
        {
            this._logger = logger;
            this.cars = carServices;
            this.competitions = competitionsServices;
            this.teams = teamServices;
            this.pilots = rallyPilots;
            this.navigators = rallyNavigators;
            this.runways = runwaysServices;
        }


        [HttpGet]
        public async Task<IActionResult> RallyЕntry(string input = null)
        {
            _logger.LogInformation("Vieew Rally Еntry");
            var user = User.Identity.Name;
            var team = await teams.FindUserAsync(user);
            var viewModel = new TeamViewModels
            {
                Id = team.Id,
                Name = team.Name,
                Cars = cars.GetCar(user),
                StartRaceDate = competitions.GetStartDate().Result.ToString("D"),
                RallyPilots = pilots.GetPilots(user),
                RallyNavigators = navigators.GetNavigators(user),
                Runway = await runways.GetRunwayForCurrentRaceAsync(),
                TeamId = await teams.GetTeamIdAsync(user),
                CompetitionId = competitions.GetCompetitionId(),
                CompetitionName = competitions.GetCompetitionName().Result,
                Text = input,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RallyЕntry(TeamViewModels input)
        {
            _logger.LogInformation("Create Rally Еntry");
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            var text = competitions.RallyЕntry(input);
            return this.RedirectToAction("RallyЕntry", "Competitions", new { input = text });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
