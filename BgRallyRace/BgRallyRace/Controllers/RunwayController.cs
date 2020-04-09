namespace BgRallyRace.Controllers
{
    using BgRallyRace.Services.Runways;
    using BgRallyRace.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize]
    public class RunwayController :Controller
    {
        private readonly IRunwaysServices runway;

        public RunwayController(IRunwaysServices runwayServices)
        {
            runway = runwayServices;
        }


        [HttpGet]
        public IActionResult Runway()
        {
            var viewModel = new RunwayViewModels
            {
                Runways = runway.GetAllRunways()
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult DetailsRunway(int id )
        {
            var runwayId = runway.GetRally(id);
            var viewModel = new RunwayViewModels
            {
                 Name = runwayId.Name,
                 TrackLength = runwayId.TrackLength,
                 Difficulty = runwayId.Difficulty,
                 Description = runwayId.Description,
                 ImagName  = runwayId.ImagName,
            };

            return this.View(viewModel);
        }
    }
}
