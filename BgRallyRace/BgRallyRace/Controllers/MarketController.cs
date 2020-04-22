namespace BgRallyRace.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using BgRallyRace.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using BgRallyRace.ViewModels;
    using BgRallyRace.Services.Market;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    [Authorize]
    public class MarketController : Controller
    {
        private readonly ILogger<MarketController> _logger;
        private readonly IMarketServices market;

        public MarketController(ILogger<MarketController> logger,IMarketServices marketServices )
        {
            this._logger = logger;
            market = marketServices;
        }

        [HttpGet]
        public async Task<IActionResult> MarketForPilots(int page =1)
        {
            _logger.LogInformation("view market pilots");
            var viewModel = new MarketViewModels
            {
                Pilots = market.GetPilotsForMarket(page),
                CurrentPage = page,
                Total = market.TotalPilots(),
            };
            return this.View(viewModel);
        }

        [HttpGet]
        public  async Task<IActionResult>  MarketForNavigators(int page = 1)
        {
            _logger.LogInformation("view market navigators");
            var viewModel = new MarketViewModels
            {
                Navigators = market.GetNavigatorsForMarket(page),
                CurrentPage = page,
                Total = market.TotalNavigators(),
            };
            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> MarketForParts(int page = 1)
        {
            _logger.LogInformation("view market parts");
            var viewModel = new MarketViewModels
            {
                Parts = market.GetPartsForMarket(page),
                CurrentPage = page,
                Total = market.TotalParts(),
            };
            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> RentalsPilot(int id)
        {
            _logger.LogInformation("rentals pilots");
            market.RentalsPilot(id, User.Identity.Name, 1000);//fixed
            return this.RedirectToAction("Pilot", "Teams");
        }

        [HttpGet]
        public async Task<IActionResult> RentalsNavigator(int id)
        {
            _logger.LogInformation("rentals navigators");
            market.RentalsNavigator(id, User.Identity.Name, 1000);//fixed
            return this.RedirectToAction("Navigator", "Teams");
        }

        [HttpGet]
        public async Task<IActionResult> RentalsParts(int id)
        {
            _logger.LogInformation("rentals parrts");
            market.RentalsPartsForCar(id, User.Identity.Name);
            return this.RedirectToAction("Car", "Cars");
        }

      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
