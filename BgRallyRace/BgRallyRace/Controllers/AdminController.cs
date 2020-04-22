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
        private readonly IRallyPilotsServices pilots;

        public AdminController(ILogger<AdminController> logger, ICreateServices createServices,
            IOpinionsServices opinionsServices, IRunwaysServices runwaysServices, IRallyPilotsServices pilotsServices)
        {
            this._logger = logger;
            this.create = createServices;
            this.opinions = opinionsServices;
            this.runways = runwaysServices;
            this.pilots = pilotsServices;
        }


        [HttpGet]
        public async Task<IActionResult> CreateRunway()
        {
            _logger.LogInformation("admin view create runway");
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRunway(RunwayViewModels input)
        {
            _logger.LogInformation("admin create runway");
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            create.CreateRunway(input);
            return this.RedirectToAction("CreateRunway", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> EditRunway(int id)
        {
            _logger.LogInformation("admin view edit runway");
            var runway = await runways.GetRunway(id);
            var viewModel = new RunwayViewModels
            {
                NameRunway = runway.Name,
                Difficulty = runway.Difficulty,
                Description = runway.Description,
                TrackLength = runway.TrackLength,
                ImagName = runway.ImagName,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditRunway(RunwayViewModels input)
        {
            _logger.LogInformation("admin edit runway");
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            var text = await runways.EditRunways(input);
            return this.RedirectToAction("DetailsRunway", "Runway", new { input = text, id = input.Id });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRunway(int id)
        {
            _logger.LogInformation("admin delete runway");
            var text = await runways.DeleteRunways(id);
            return this.RedirectToAction("Runway", "Runway", new { input = text});
        }

        [HttpGet]
        public async Task<IActionResult> Pilots(int page = 1)
        {
            _logger.LogInformation("admin view pilot");
            var viewModel = new PilotViewModels
            {
                Pilots = pilots.GetPeople(),
                CurrentPage = page,
                Total = pilots.TotalPilots(),
            };
            return this.View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> CreatePilot()
        {
            _logger.LogInformation("admin view create pilot");
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePilot(PilotViewModels input)
        {
            _logger.LogInformation("admin create pilot");
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            create.CreatePilot(input);
            return this.RedirectToAction("CreatePilot", "Admin");
        }


        [HttpGet]
        public async Task<IActionResult> EditPilot(int id)
        {
            _logger.LogInformation("admin view edit pilot");
            var pilot =  pilots.GetPilot(id);
            var viewModel = new PilotViewModels
            {
                FirstName = pilot.FirstName,
                LastName = pilot.LastName,
                Age = pilot.Age,
                Salary = pilot.Salary,
                Concentration = pilot.Concentration,
                Devotion = pilot.Devotion,
                Reflexes = pilot.Reflexes,
                Energy = pilot.Energy,
                Experience = pilot.Experience,
                PhysicalTraining = pilot.PhysicalTraining,
                Pounds = pilot.Pounds,
            };

            return this.View(viewModel);
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
