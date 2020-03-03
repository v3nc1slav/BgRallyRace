namespace BgRallyRace.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using BgRallyRace.Data;
    using BgRallyRace.Services;
    using Microsoft.AspNetCore.Authorization;
    using BgRallyRace.Services.Training;

    public class TrainingController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext db { get; set; } = new ApplicationDbContext();
        private IRallyPilotsServices pilot { get; set; } 
        private IRallyNavigatorsServices navigator { get; set; }
        public ITrainingServices training { get; set; }

        public TrainingController(ILogger<HomeController> logger, IRallyPilotsServices dbPilot, 
            IRallyNavigatorsServices dbNavigator, ITrainingServices dbTraining)
        {
            _logger = logger;
            pilot = dbPilot;
            navigator = dbNavigator;
            training = dbTraining;
        }


        [Authorize]
        [HttpPost]
        public IActionResult Training(string values)
        {
            var pilots = TempData["Pilots"] as int[] ;
            var navigators = TempData["Navigator"] as int[];
            training.Training(0, values);
            return this.RedirectToAction("Home", "Index");
        }

    }
}
