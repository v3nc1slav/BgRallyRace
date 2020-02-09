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
        public TeamController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Team()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateTeam(string textTeam)
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
