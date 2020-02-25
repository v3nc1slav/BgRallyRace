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
    public class TrainingController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext db { get; set; } = new ApplicationDbContext();
        private IRallyPilotsServices pilot { get; set; } 
        private IRallyNavigatorsServices navigator { get; set; }

        public TrainingController(ILogger<HomeController> logger, IRallyPilotsServices dbPilot, 
            IRallyNavigatorsServices dbNavigator)
        {
            _logger = logger;
            pilot = dbPilot;
            navigator = dbNavigator;
        }
        public IActionResult Training(int Id)
        {
            var pilots = TempData["Pilots"] as int[] ;
            return this.View();
        }

        public IActionResult Dismissal(int id)
        {
            pilot.Fired(id);
            return this.RedirectToAction("Pilot", "Team");
        }
    }
}
