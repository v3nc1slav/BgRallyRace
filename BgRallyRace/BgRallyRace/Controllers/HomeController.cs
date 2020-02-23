using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BgRallyRace.Models;
using Microsoft.AspNetCore.Authorization;
using BgRallyRace.Services;
using Microsoft.AspNetCore.Http;
using BgRallyRace.ViewModels;

namespace BgRallyRace.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOpinionsServices opinionsServices;
        public readonly ITeamServices teamServices;

        public HomeController(ILogger<HomeController> logger, ITeamServices teamServices, 
            IOpinionsServices opinionsServices)
        {
            _logger = logger;
            this.teamServices = teamServices;
            this.opinionsServices = opinionsServices;
        }

        public IActionResult Index()
        {
            var viewModel = new TeamViewModels
            {
                Team = teamServices.FindUser(User.Identity.Name)
            };
            return View(viewModel);
        }

        public IActionResult Opinion()
        {
            var viewModel = new OpinionsViewModels
            {
                Opinions = opinionsServices.GetOpinions()
            };
            return this.View(viewModel);
        }

        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult Gallery()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Contact(string content)
        {
            opinionsServices.AddOpinionAsync(content, User.Identity.Name);
            return RedirectToAction("Opinion", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
