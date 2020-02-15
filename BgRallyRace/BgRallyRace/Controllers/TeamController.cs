using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BgRallyRace.Models;
using Microsoft.AspNetCore.Authorization;
using BgRallyRace.Data;
using BgRallyRace.Models.Home;
using BgRallyRace.Services;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using BgRallyRace.ViewModels;
using BgRallyRace.Models.RandomName;

namespace BgRallyRace.Controllers
{
    public class TeamController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext db { get; set; } = new ApplicationDbContext();
        private IRallyPilotsServices pilot { get; set; } 
        private IRallyNavigatorsServices navigator { get; set; }
        private ICarServices car { get; set; }

        public TeamController(ILogger<HomeController> logger, IRallyPilotsServices dbPilot, 
            IRallyNavigatorsServices dbNavigator, ICarServices dbCar )
        { 
            _logger = logger;
            pilot = dbPilot;
            navigator = dbNavigator;
            car = dbCar;
        }

        [Authorize]
        public IActionResult Pilot()
        {
            var viewModel = new PilotViewModels
            {
                Pilots = pilot.GetPilots(User.Identity.Name)
            };
            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Car()
        {
            var viewModel = new CarViewModels
            {
                Engines = car.GetCar(User.Identity.Name)
            };
            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Navigator()
        {
            var viewModel = new NavigatorViewModels
            {
                Navigators = navigator.GetNavigators(User.Identity.Name)
            };
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task< IActionResult> CreateTeam(string textTeam)
        {
            var team = new TeamServices(db);
            await team.CreateTeam(textTeam, User.Identity.Name);
            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
