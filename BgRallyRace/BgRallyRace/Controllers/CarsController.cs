namespace BgRallyRace.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using BgRallyRace.Services;
    using BgRallyRace.ViewModels;

    public class CarsController : Controller
    {
        private readonly ICarServices car;

        public CarsController(ICarServices carServices)
        {
            this.car = carServices;
        }

        [Authorize]
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

        [Authorize]
        [HttpPost]
        public IActionResult Repair(string type, int id)
        {
            car.Repair(type, id);
            return this.Car();
        }


    }
}
