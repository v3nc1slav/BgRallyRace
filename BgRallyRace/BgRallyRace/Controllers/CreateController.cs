namespace BgRallyRace.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using BgRallyRace.Models.Enums;
    using BgRallyRace.Services.Create;

    //[Authorize(Roles = "Administrator")]
    public class CreateController : Controller
    {
        private readonly ICreateServices create;

        public CreateController(ICreateServices createServices)
        {
            this.create = createServices;
        }

        public IActionResult Runway()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateRunway(string name, decimal length, DifficultyType difficulty, string description)
        {
            create.CreateRunwayServices(name, length, difficulty, description);
            return RedirectToAction("Runway", "Create");
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
            return RedirectToAction("Pilot", "Create");
        }
    }
}
