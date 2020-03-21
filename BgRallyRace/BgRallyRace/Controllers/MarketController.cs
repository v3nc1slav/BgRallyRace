﻿namespace BgRallyRace.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using BgRallyRace.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using BgRallyRace.ViewModels;
    using BgRallyRace.Services.Market;

    [Authorize]
    public class MarketController : Controller
    {
        private readonly IMarketServices market;

        public MarketController(IMarketServices marketServices )
        {
            market = marketServices;
        }

        [HttpGet]
        public IActionResult MarketForPilots(int page =1)
        {
            var viewModel = new MarketViewModels
            {
                Pilots = market.GetPilotsForMarket(page),
                CurrentPage = page,
                Total = market.TotalPilots(),
            };
            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult MarketForNavigators(int page = 1)
        {
            var viewModel = new MarketViewModels
            {
                Navigators = market.GetNavigatorsForMarket(page),
                CurrentPage = page,
                Total = market.TotalNavigators(),
            };
            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult MarketForParts()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult RentalsPilot(int id)
        {
            market.RentalsPilot(id, User.Identity.Name, 1000);
            return this.RedirectToAction("Pilot", "Teams");
        }

        [HttpGet]
        public IActionResult RentalsNavigator(int id)
        {
            market.RentalsNavigator(id, User.Identity.Name, 1000);
            return this.RedirectToAction("Navigator", "Teams");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
