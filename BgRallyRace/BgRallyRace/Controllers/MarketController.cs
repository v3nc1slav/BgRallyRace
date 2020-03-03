namespace BgRallyRace.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using BgRallyRace.Models;
    using Microsoft.AspNetCore.Authorization;
    using BgRallyRace.Data;
    using BgRallyRace.Services;
    using Microsoft.AspNetCore.Http;
    using BgRallyRace.ViewModels;
    using BgRallyRace.Services.Market;

    public class MarketController : Controller
    {
        private readonly IMarketServices market;

        public MarketController(IMarketServices marketServices )
        {
            market = marketServices;
        }

        [Authorize]
        public IActionResult MarketForPilots()
        {
            var viewModel = new MarketViewModels
            {
                Pilots = market.GetPilotsForMarket()
            };
            return this.View(viewModel);
        }


        [Authorize]
        public IActionResult MarketForNavigators()
        {
            var viewModel = new MarketViewModels
            {
                Navigators = market.GetNavigatorsForMarket()
            };
            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult MarketForParts()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult RentalsPilot(int id)
        {
            market.RentalsPilot(8, User.Identity.Name, 1000);
            return this.RedirectToAction("Pilot", "Teams");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
