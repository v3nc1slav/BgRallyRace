using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BgRallyRace.Models;
using Microsoft.AspNetCore.Authorization;
using BgRallyRace.Data;
using BgRallyRace.Services;
using System.IO;
using System.Text;
using BgRallyRace.ViewModels;
using BgRallyRace.Models.PartsCar;

namespace BgRallyRace.Controllers
{
    public class CarsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICarServices car;

        private ApplicationDbContext db { get; set; } = new ApplicationDbContext();

        public CarsController(ILogger<HomeController> logger, ICarServices carServices)
        {
            _logger = logger;
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
