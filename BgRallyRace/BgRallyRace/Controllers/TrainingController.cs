namespace BgRallyRace.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using BgRallyRace.Data;
    using BgRallyRace.Services;
    using Microsoft.AspNetCore.Authorization;
    using BgRallyRace.Services.Training;
    using System.Linq;


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
        public IActionResult Training(string sessionType, string type)
        {
            _logger.LogInformation("Training");
            var inputSessionType = sessionType.Split().ToArray();
            var typeTreining = inputSessionType[0];
            int money = int.Parse(inputSessionType[1]);

            var input = type.Split().ToArray();
            type = input[0];
            int id = int.Parse(input[1]);

            if (type == "Pilot" && (pilot.IsItBusy(id)==true) )
            {
                return this.RedirectToAction($"{type}", "Teams");
            }
            else if (type == "Navigator" && (navigator.IsItBusy(id) == true))
            {
                return this.RedirectToAction($"{type}", "Teams");
            }
           
            training.Training(id, typeTreining, type);
            this.money.ExpenseAccountAsync(money, User.Identity.Name);

            return this.RedirectToAction($"{type}","Teams");
        }

    }
}

