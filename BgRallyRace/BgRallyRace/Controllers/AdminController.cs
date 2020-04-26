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
    using BgRallyRace.Services.Competitions;

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ICreateServices create;
        private readonly IOpinionsServices opinions;
        private readonly IRunwaysServices runways;
        private readonly IRallyPilotsServices pilots;
        private readonly IEditServices edit;
        private readonly IDeleteServices delete;
        private readonly ICompetitionsServices competitions;

        public AdminController(ILogger<AdminController> logger, ICreateServices createServices,
            IOpinionsServices opinionsServices, IRunwaysServices runwaysServices, IRallyPilotsServices pilotsServices, IEditServices editServices,
            IDeleteServices deleteServices, ICompetitionsServices competitionsServices)
        {
            this._logger = logger;
            this.create = createServices;
            this.opinions = opinionsServices;
            this.runways = runwaysServices;
            this.pilots = pilotsServices;
            this.edit = editServices;
            this.delete = deleteServices;
            this.competitions = competitionsServices;
        }


        [HttpGet]
        public async Task<IActionResult> CreateRunway(string? input)
        {
            _logger.LogInformation("admin view create runway");
            var viewModel = new RunwayViewModels
            {
                Text = input,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRunway(RunwayViewModels input)
        {
            _logger.LogInformation("admin create runway");
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            var text = await create.CreateRunwayAsync(input);
            return this.RedirectToAction("CreateRunway", "Admin", new { input = text});
        }

        [HttpGet]
        public async Task<IActionResult> EditRunway(int id)
        {
            _logger.LogInformation("admin view edit runway");
            var runway = await runways.GetRunwayAsync(id);
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
            var text = await edit.EditRunways(input);
            return this.RedirectToAction("DetailsRunway", "Runway", new { input = text, id = input.Id });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRunway(int id)
        {
            _logger.LogInformation("admin delete runway");
            var text = await delete.DeleteRunways(id);
            return this.RedirectToAction("Runway", "Runway", new { input = text});
        }

        [HttpGet]
        public async Task<IActionResult> Pilots(int page = 1, string input = null)
        {
            _logger.LogInformation("admin view pilot");
            var viewModel = new PilotViewModels
            {
                Pilots = pilots.GetPeople(page),
                CurrentPage = page,
                Total = pilots.TotalPilots(),
                Text = input,
            };
            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreatePilot(string input = null)
        {
            _logger.LogInformation("admin view create pilot");
            var viewModel = new PilotViewModels
            {
                Text=input
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePilot(PilotViewModels input)
        {
            _logger.LogInformation("admin create pilot");
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            var text = await create.CreatePilotAsync(input);
            return this.RedirectToAction("CreatePilot", "Admin", new { input = text});
        }

        [HttpGet]
        public async Task<IActionResult> EditPilot(int id)
        {
            _logger.LogInformation("admin view edit pilot");
            var pilot =  pilots.GetPilot(id);
            var viewModel = new PilotViewModels
            {
                Id = pilot.Id,
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

        [HttpPost]
        public async Task<IActionResult> EditPilot(PilotViewModels input)
        {
            _logger.LogInformation("admin edit pilot");
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            var text = await edit.EditPilot(input);
            return this.RedirectToAction("Pilots", "Admin", new {input = text });
        }

        [HttpGet]
        public async Task<IActionResult> DeletePilot(int id)
        {
            _logger.LogInformation("admin delete pilot");
            var text = await delete.DeletePilots(id);
            return this.RedirectToAction("Pilots", "Admin", new { input = text });
        }

        [HttpGet]
        public async Task<IActionResult> Navigator(string input = null)
        {
            _logger.LogInformation("admin view navigator");
            var viewModel = new NavigatorViewModels
            {
                Text = input,
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateNavigator(string input = null)
        {
            _logger.LogInformation("admin view navigator");
            var viewModel = new NavigatorViewModels
            {
                Text = input,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNavigator(NavigatorViewModels input)
        {
            _logger.LogInformation("admin create navigator");
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            var text = await create.CreateNavigatorAsync(input);
            return this.RedirectToAction("CreateNavigator", "Admin", new { input = text });
        }

        [HttpGet]
        public async Task<IActionResult> Opinions()
        {
            _logger.LogInformation("admin view opinions");
            var viewModel = new OpinionsViewModels
            {
                OpinionsForAdmin = await opinions.GetOpinionsForAdminAsync(),
                CountNotAuthorization = opinions.GetCountNotAuthorization(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AuthorizationOpinions(int[] opinionsVisible, int[] opinionsInvisible)
        {
            _logger.LogInformation("admin authorization opinions");
           await opinions.MadeOpinionsVisibleAsync(opinionsVisible);
           await opinions.MadeOpinionsInvisibleAsync(opinionsInvisible);
            return RedirectToAction("Opinions", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Parts(string input = null)
        {
            _logger.LogInformation("admin view create parts");
            var viewModel = new PartsViewModels
            {
                Text = input,
            };
            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateParts(string input = null)
        {
            _logger.LogInformation("admin view create parts");
            var viewModel = new PartsViewModels
            {
                Text = input,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateParts(PartsViewModels input)
        {
            _logger.LogInformation("admin creat opinions");
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            var text = create.CreateParts(input);
            return this.RedirectToAction("CreateParts", "Admin", new {input = text });
        }

        [HttpGet]
        public async Task<IActionResult> Competitions(string input = null, int page = 1)
        {
            _logger.LogInformation("admin view competitions");
            var view = new CompetitionsViewModels
            {
                Competitions = await competitions.GetAllCompetitions(page),
                Text = input,
                CurrentPage = page,
                Total = competitions.TotalPage(),
            };
            return this.View(view);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCompetitions(string input = null)
        {
            _logger.LogInformation("admin view creat competitions");
            var view = new CompetitionsViewModels
            {
                Runways = await runways.GetAllRunwaysAsync(),
                Text = input,
            };
            return this.View(view);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompetitions(CompetitionsViewModels input)
        {
            _logger.LogInformation("admin create opinions");
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            await create.CreateCompetitionsAsync(input);
            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> EditCompetitions(int id)
        {
            _logger.LogInformation("admin view edit competitions");
            var competition = await competitions.GetCompetition(id);
            var viewModel = new CompetitionsViewModels
            {
                Id = competition.Id,
                Name = competition.Name,
                PrizeFund = competition.PrizeFund,
                Stages = competition.Stages,
                Runways = await runways.GetAllRunwaysAsync(),
                StartRaceDate = competition.StartRaceDate,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditCompetitions(CompetitionsViewModels input)
        {
            _logger.LogInformation("admin edit competitions");
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            var text = await edit.EditCompetitions(input);
            return this.RedirectToAction("Competitions", "Admin", new { input = text });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
