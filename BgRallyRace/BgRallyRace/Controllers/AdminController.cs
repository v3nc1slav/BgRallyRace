namespace BgRallyRace.Controllers
{
    using System.Threading.Tasks;
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Logging;
    using BgRallyRace.Services.Admin;
    using BgRallyRace.Services;
    using BgRallyRace.ViewModels;
    using BgRallyRace.Services.Runways;
    using BgRallyRace.Models;

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ICreateServices create;
        private readonly IOpinionsServices opinions;
        private readonly IRunwaysServices runways;

        public AdminController(ILogger<AdminController> logger, ICreateServices createServices,
            IOpinionsServices opinionsServices, IRunwaysServices runwaysServices)
        {
            this._logger = logger;
            this.create = createServices;
            this.opinions = opinionsServices;
            this.runways = runwaysServices;
        }

        [HttpGet]
        public async Task<IActionResult> Runway()
        {
            _logger.LogInformation("admin view runway");
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Runway(RunwayViewModels input)
        {
            _logger.LogInformation("admin create runway");
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            create.CreateRunway(input);
            return this.RedirectToAction("Runway", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Pilot()
        {
            _logger.LogInformation("admin view pilot");
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Pilot(PilotViewModels input)
        {
            _logger.LogInformation("admin creat pilot");
            if (!this.ModelState.IsValid )
            {
                return this.View(input);
            }
            create.CreatePilot(input);
            return this.RedirectToAction("Pilot", "Admin");
        }

        [HttpGet]

        public async Task<IActionResult> Navigator()
        {
            _logger.LogInformation("admin view navigator");
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Navigator(NavigatorViewModels input)
        {
            _logger.LogInformation("admin create navigator");
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            create.CreateNavigator(input);
            return this.RedirectToAction("Navigator", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Opinions()
        {
            _logger.LogInformation("admin view opinions");
            var viewModel = new OpinionsViewModels
            {
                OpinionsForAdmin = opinions.GetOpinionsForAdmin(),
                CountNotAuthorization = opinions.GetCountNotAuthorization(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AuthorizationOpinions(int[] opinionsVisible, int[] opinionsInvisible)
        {
            _logger.LogInformation("admin authorization opinions");
            opinions.MadeOpinionsVisible(opinionsVisible);
            opinions.MadeOpinionsInvisible(opinionsInvisible);
            return RedirectToAction("Opinions", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Parts()
        {
            _logger.LogInformation("admin view parts");
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Parts(PartsViewModels input)
        {
            _logger.LogInformation("admin creat opinions");
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            create.CreateParts(input);
            return this.RedirectToAction("Parts", "Admin");
        }


        [HttpGet]
        public async Task<IActionResult> Competitions()
        {
            _logger.LogInformation("admin view competitions");
            var view = new CompetitionsViewModels
            {
                Runways = runways.GetAllRunways()
            };
            return this.View(view);
        }

        [HttpPost]
        public async Task<IActionResult> Competitions(CompetitionsViewModels input)
        {
            _logger.LogInformation("admin create opinions");
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            create.CreateCompetitions(input);
            return this.RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
