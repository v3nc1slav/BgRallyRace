using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BgRallyRace.Models;
using Microsoft.AspNetCore.Authorization;
using BgRallyRace.Data;
using BgRallyRace.Services;
using Microsoft.AspNetCore.Http;
using BgRallyRace.ViewModels;

namespace BgRallyRace.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext db { get; set; } = new ApplicationDbContext();
        private IRallyPilotsServices pilot { get; set; } 
        private IRallyNavigatorsServices navigator { get; set; }

        public TeamsController(ILogger<HomeController> logger, IRallyPilotsServices dbPilot, 
            IRallyNavigatorsServices dbNavigator )
        { 
            _logger = logger;
            pilot = dbPilot;
            navigator = dbNavigator;
        }

        [Authorize]
        public IActionResult Pilot()
        {
            var viewModel = new PilotViewModels
            {
                Pilots = pilot.GetPilots(User.Identity.Name)
            };
            TempData["Pilots"] = viewModel.Pilots.Select(x => x.Id).ToArray();
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
        public  IActionResult CreateTeam(string textTeam)
        {
            var team = new TeamServices(db);
            team.CreateTeam(textTeam, User.Identity.Name);
            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
