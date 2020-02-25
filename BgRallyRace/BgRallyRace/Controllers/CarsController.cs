namespace BgRallyRace.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using BgRallyRace.Services;
    using BgRallyRace.ViewModels;
    using BgRallyRace.Models.PartsCar;
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
            var carId = car.GetCarId(User.Identity.Name);
            var aerodynamics = car.GetAerodynamics(User.Identity.Name);
            var brakes = car.GetBrakes(User.Identity.Name);
            var engines = car.GetEngine(User.Identity.Name);
            var garboxs = car.GetGearboxs(User.Identity.Name);
            var modelsCars = car.GetModelsCars(User.Identity.Name);
            var mountings = car.GetMountings(User.Identity.Name);
            var turbo = car.GetTurbo(User.Identity.Name);
            var maxSpeed = car.GetMaxSpeed(aerodynamics, brakes, engines, garboxs,
                modelsCars, mountings, turbo);

            var viewModel = new CarViewModels
            {
                CarId = carId,
                Aerodynamics = aerodynamics,
                Brakes = brakes,
                Engines = engines,
                Gearboxs = garboxs,
                ModelsCars = modelsCars,
                Mountings = mountings,
                Turbo = turbo,
                MaxSpeed = maxSpeed,
            };
            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Repair(Parts repair)
        {
            return RedirectToAction("Car", "Cars");
        }


    }
}
