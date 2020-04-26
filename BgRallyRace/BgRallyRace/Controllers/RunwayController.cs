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
        public async Task<IActionResult> Runway(string input=null)
        {
            _logger.LogInformation("view runways");
            var viewModel = new RunwayViewModels
            {
                Runways = await runway.GetAllRunwaysAsync(),
                Text = input,
            };
            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DetailsRunway(int id, string input = null )
        {
            _logger.LogInformation("view details runway");
            var runwayId = await runway.GetRunwayAsync(id);
            var viewModel = new RunwayViewModels
            {
                 Id = runwayId.Id,
                 NameRunway = runwayId.Name,
                 TrackLength = runwayId.TrackLength,
                 Difficulty = runwayId.Difficulty,
                 Description = runwayId.Description,
                 ImagName  = runwayId.ImagName,
                 Text = input,
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
