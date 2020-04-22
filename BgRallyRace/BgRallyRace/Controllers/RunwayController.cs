namespace BgRallyRace.Controllers
{
    using BgRallyRace.Models;
    using BgRallyRace.Services.Runways;
    using BgRallyRace.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Diagnostics;
    using System.Threading.Tasks;

    [Authorize]
    public class RunwayController :Controller
    {
        private readonly ILogger<RunwayController> _logger;
        private readonly IRunwaysServices runway;

        public RunwayController(ILogger<RunwayController> logger,IRunwaysServices runwayServices)
        {
            this._logger = logger;
            runway = runwayServices;
        }


        [HttpGet]
        public async Task<IActionResult> Runway()
        {
            _logger.LogInformation("view runways");
            var viewModel = new RunwayViewModels
            {
                Runways = runway.GetAllRunways()
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DetailsRunway(int id )
        {
            _logger.LogInformation("view details runway");
            var runwayId = runway.GetRally(id);
            var viewModel = new RunwayViewModels
            {
                 NameRunway = runwayId.Name,
                 TrackLength = runwayId.TrackLength,
                 Difficulty = runwayId.Difficulty,
                 Description = runwayId.Description,
                 ImagName  = runwayId.ImagName,
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
