namespace BgRallyRace.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using BgRallyRace.Models.Enums;
    using BgRallyRace.Services.Admin;
    using BgRallyRace.Services;
    using BgRallyRace.ViewModels;

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ICreateServices create;

        private readonly IOpinionsServices opinions;

        public AdminController(ICreateServices createServices, IOpinionsServices opinionsServices)
        {
            this.create = createServices;
            this.opinions = opinionsServices;
        }

        [HttpGet]
        public IActionResult Runway()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateRunway(string name, decimal length, DifficultyType difficulty, string description)
        {
            create.CreateRunway(name, length, difficulty, description);
            return RedirectToAction("Runway", "Admin");
        }

        [HttpGet]
        public IActionResult Pilot()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreatePilot(string? firstName, string? lastName, int age, int concentration, int experience,
            int energy, int devotion, int physicalTraining, int pounds, int salary, int reflexes)
        {
            create.CreatePilot(firstName, lastName, age, concentration, experience, energy, devotion, physicalTraining,
                pounds, salary, reflexes);
            return RedirectToAction("Pilot", "Admin");
        }

        [HttpGet]

        public IActionResult Navigator()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateNavigator(string? firstName, string? lastName, int age, int concentration, int experience,
            int energy, int devotion, int physicalTraining, int pounds, int salary, int communication)
        {
            create.CreateNavigator(firstName, lastName, age, concentration, experience, energy, devotion, physicalTraining,
                pounds, salary, communication);
            return RedirectToAction("Navigator", "Admin");
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

        [HttpGet]
        public IActionResult Parts()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateParts(string name, decimal length, DifficultyType difficulty, string description)
        {
            create.CreateRunway(name, length, difficulty, description);
            return RedirectToAction("Runway", "Admin");
        }

        [HttpPost]
        public IActionResult AuthorizationOpinions(int[] opinionsVisible, int[] opinionsInvisible)
        {
            opinions.MadeOpinionsVisible(opinionsVisible);
            opinions.MadeOpinionsInvisible(opinionsInvisible);
            return RedirectToAction("Opinions", "Admin");
        }
    }
}
