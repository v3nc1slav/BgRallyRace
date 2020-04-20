namespace BgRallyRace.Controllers
{
    using BgRallyRace.Services.Competitions;
    using BgRallyRace.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize]
    public class RaceHistoryController : Controller
    {
        private readonly IRaceHistoryServices history;

        public RaceHistoryController(IRaceHistoryServices raceHistoryServices)
        {
            this.history = raceHistoryServices;
        }

        [HttpGet]
        public IActionResult History()
        {
            var viewModel = new HistoryViewModels
            {
                History = history.GetHistory()
            };
            return this.View(viewModel);
        }
    }
}
