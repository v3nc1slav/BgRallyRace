namespace BgRallyRace.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using BgRallyRace.Services;
    using BgRallyRace.ViewModels;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using BgRallyRace.Models;
    using System.Diagnostics;

    [Authorize]
    public class CarsController : Controller
    {
        private readonly ILogger<CarsController> _logger;
        private readonly ICarServices car;

        public CarsController(ILogger<CarsController> logger, ICarServices carServices)
        {
            this._logger = logger;
            this.car = carServices;
        }

        [HttpGet]
        public async Task<IActionResult> Car(string input=null)
        {
            _logger.LogInformation("view car");
            var viewModel = new CarViewModels
            {
                CarId = car.GetCarId(User.Identity.Name),
                Aerodynamics = car.GetAerodynamics(User.Identity.Name),
                Brakes = car.GetBrakes(User.Identity.Name),
                Engines = car.GetEngine(User.Identity.Name),
                Gearboxs = car.GetGearboxs(User.Identity.Name),
                ModelsCars = car.GetModelsCars(User.Identity.Name),
                Mountings = car.GetMountings(User.Identity.Name),
                Turbo = car.GetTurbo(User.Identity.Name),
                MaxSpeed = car.GetMaxSpeed(User.Identity.Name),
                Text = input,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Repair(string type, int id)
        {
            _logger.LogInformation("repair car");
            var input = type.Split().ToArray();
            type = input[0];
            decimal price = decimal.Parse(input[1]);
            string user = input[2];
            var text = car.Repair(type, id, price, user);
            return this.RedirectToAction("Car", "Cars", new {input = text });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
