namespace BgRallyRace.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using BgRallyRace.Models.Enums;
    using BgRallyRace.Services.Admin;
    using BgRallyRace.Services;
    using BgRallyRace.ViewModels;

    //[Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly ICreateServices create;

        private readonly IOpinionsServices opinions;

        public AdminController(ICreateServices createServices, IOpinionsServices opinionsServices)
        {
            this.create = createServices;
            this.opinions = opinionsServices;
        }

        public IActionResult Runway()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateRunway(string name, decimal length, DifficultyType difficulty, string description)
        {
            create.CreateRunwayServices(name, length, difficulty, description);
            return RedirectToAction("Runway", "Admin");
        }

        public IActionResult Pilot()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreatePilot(string? firstName, string? lastName, int age, int concentration, int experience,
            int energy, int devotion, int physicalTraining, int pounds, int salary, int reflexes)
        {
            create.CreatePilotServices(firstName, lastName, age, concentration, experience, energy, devotion, physicalTraining,
                pounds, salary, reflexes);
            return RedirectToAction("Pilot", "Admin");
        }

        public IActionResult Opinions()
        {
            var viewModel = new OpinionsViewModels
            {
                OpinionsForAdmin = opinions.GetOpinionsForAdmin()
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
    }
}
