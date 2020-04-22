namespace BgRallyRace.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using BgRallyRace.Services;
    using Microsoft.AspNetCore.Authorization;
    using BgRallyRace.Services.Training;
    using System.Linq;
    using System.Threading.Tasks;
    using BgRallyRace.Models;
    using System.Diagnostics;

    [Authorize]
    public class TrainingController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITrainingServices training;
        private readonly IMoneyAccountServices money;
        private readonly IRallyPilotsServices pilot;
        private readonly IRallyNavigatorsServices navigator;

        public TrainingController(ILogger<HomeController> logger, ITrainingServices dbTraining,
            IMoneyAccountServices moneyAccountServices, IRallyPilotsServices rallyPilotsServices, IRallyNavigatorsServices rallyNavigatorsServices)
        {
            _logger = logger;
            training = dbTraining;
            this.money = moneyAccountServices;
            this.pilot = rallyPilotsServices;
            this.navigator = rallyNavigatorsServices;
        }


        [HttpPost]
        public async Task<IActionResult> Training(string sessionType, string type)
        {
            _logger.LogInformation("Training");
            var inputSessionType = sessionType.Split().ToArray();
            var typeTreining = inputSessionType[0];
            int money = int.Parse(inputSessionType[1]);

            var input = type.Split().ToArray();
            type = input[0];
            int id = int.Parse(input[1]);
            var text = string.Empty;

            if (type == "Pilot" && (pilot.IsItBusy(id)==true) )
            {
                text = "Пилота, вече е тренирал или е зает с друга дейност.";
                return this.RedirectToAction($"{type}", "Teams", new {input = text });
            }
            else if (type == "Navigator" && (navigator.IsItBusy(id) == true))
            {
                text = "Навигатора, вече е тренирал или е зает с друга дейност.";
                return this.RedirectToAction($"{type}", "Teams", new { input = text });
            }
           
            training.Training(id, typeTreining, type);
            this.money.ExpenseAccountAsync(money, User.Identity.Name);

            text = "Тренировката бе извършена успешно.";
            return this.RedirectToAction($"{type}","Teams", new { input = text });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

