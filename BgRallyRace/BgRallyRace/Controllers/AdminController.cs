﻿namespace BgRallyRace.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using BgRallyRace.Services.Admin;
    using BgRallyRace.Services;
    using BgRallyRace.ViewModels;

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ICreateServices create;

        private readonly IOpinionsServices opinions;

        public AdminController(ICreateServices createServices, IOpinionsServices opinionsServices)
        {
            this.create = createServices;
            this.opinions = opinionsServices;
        }

        [HttpGet]
        public IActionResult Runway()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Runway(RunwayViewModels input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            create.CreateRunway(input);
            return this.RedirectToAction("Runway", "Admin");
        }

        [HttpGet]
        public IActionResult Pilot()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Pilot(PilotViewModels input)
        {
            if (!this.ModelState.IsValid )
            {
                return this.View(input);
            }
            create.CreatePilot(input);
            return this.RedirectToAction("Pilot", "Admin");
        }

        [HttpGet]

        public IActionResult Navigator()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Navigator(NavigatorViewModels input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            create.CreateNavigator(input);
            return this.RedirectToAction("Navigator", "Admin");
        }

        [HttpGet]
        public IActionResult Opinions()
        {
            var viewModel = new OpinionsViewModels
            {
                OpinionsForAdmin = opinions.GetOpinionsForAdmin(),
                CountNotAuthorization = opinions.GetCountNotAuthorization(),
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Parts()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Parts(PartsViewModels input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            create.CreateParts(input);
            return this.RedirectToAction("Parts", "Admin");
        }

        [HttpPost]
        public IActionResult AuthorizationOpinions(int[] opinionsVisible, int[] opinionsInvisible)
        {
            opinions.MadeOpinionsVisible(opinionsVisible);
            opinions.MadeOpinionsInvisible(opinionsInvisible);
            return RedirectToAction("Opinions", "Admin");
        }
    }
}
