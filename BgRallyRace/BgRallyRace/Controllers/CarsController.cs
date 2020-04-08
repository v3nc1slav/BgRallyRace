namespace BgRallyRace.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using BgRallyRace.Services;
    using BgRallyRace.ViewModels;
    using System.Linq;

    [Authorize]
    public class CarsController : Controller
    {
        private readonly ICarServices car;

        public CarsController(ICarServices carServices)
        {
            this.car = carServices;
        }

        [HttpGet]
        public IActionResult Car()
        {
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
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Repair(string type, int id)
        {
            var input = type.Split().ToArray();
            type = input[0];
            decimal price = decimal.Parse(input[1]);
            string user = input[2];
            car.Repair(type, id, price, user);
            return this.Car();
        }
    }
}
