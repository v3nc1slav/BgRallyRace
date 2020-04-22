namespace BgRallyRace.Controllers
{
    using BgRallyRace.Models;
    using BgRallyRace.Services.Competitions;
    using BgRallyRace.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Diagnostics;
    using System.Threading.Tasks;

    [Authorize]
    public class RaceHistoryController : Controller
    {
        private readonly IRaceHistoryServices history;
        private readonly ILogger<RaceHistoryController> _logger;

        public RaceHistoryController(ILogger<RaceHistoryController> logger,IRaceHistoryServices raceHistoryServices)
        {
            this._logger = logger;
            this.history = raceHistoryServices;
        }

        [HttpGet]
        public async Task<IActionResult> History()
        {
            _logger.LogInformation("View history");
            var viewModel = new HistoryViewModels
            {
                History = history.GetHistory()
            };
            return this.View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
