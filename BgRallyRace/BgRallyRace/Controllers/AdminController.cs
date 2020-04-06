namespace BgRallyRace.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using BgRallyRace.Services.Admin;
    using BgRallyRace.Services;
    using BgRallyRace.ViewModels;
    using BgRallyRace.Services.Runways;

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ICreateServices create;
        private readonly IOpinionsServices opinions;
        private readonly IRunwaysServices runways;

        public AdminController(ICreateServices createServices, IOpinionsServices opinionsServices, IRunwaysServices runwaysServices)
        {
            this.create = createServices;
            this.opinions = opinionsServices;
            this.runways = runwaysServices;
        }

        [HttpGet]
        public IActionResult Runway()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Runway(RunwayViewModels input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            create.CreateRunway(input);
            return this.RedirectToAction("Runway", "Admin");
        }

        [HttpGet]
        public IActionResult Pilot()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Pilot(PilotViewModels input)
        {
            if (!this.ModelState.IsValid )
            {
                return this.View(input);
            }
            create.CreatePilot(input);
            return this.RedirectToAction("Pilot", "Admin");
        }

        [HttpGet]

        public IActionResult Navigator()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Navigator(NavigatorViewModels input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            create.CreateNavigator(input);
            return this.RedirectToAction("Navigator", "Admin");
        }

        [HttpGet]
        public IActionResult Opinions()
        {
            var viewModel = new OpinionsViewModels
            {
                OpinionsForAdmin = opinions.GetOpinionsForAdmin(),
                CountNotAuthorization = opinions.GetCountNotAuthorization(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult AuthorizationOpinions(int[] opinionsVisible, int[] opinionsInvisible)
        {
            opinions.MadeOpinionsVisible(opinionsVisible);
            opinions.MadeOpinionsInvisible(opinionsInvisible);
            return RedirectToAction("Opinions", "Admin");
        }

        [HttpGet]
        public IActionResult Parts()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Parts(PartsViewModels input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            create.CreateParts(input);
            return this.RedirectToAction("Parts", "Admin");
        }


        [HttpGet]
        public IActionResult Competitions()
        {
            var view = new CompetitionsViewModels
            {
                Runways = runways.GetALLRunways()
            };
            return this.View(view);
        }

        [HttpPost]
        public IActionResult Competitions(CompetitionsViewModels input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            create.CreateCompetitions(input);
            return this.RedirectToAction("Index", "Home");
        }

    }
}
