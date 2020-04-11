﻿namespace BgRallyRace.Controllers
{
    using BgRallyRace.Services;
    using BgRallyRace.Services.Competitions;
    using BgRallyRace.Services.Runways;
    using BgRallyRace.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CompetitionsController : Controller
    {
        private readonly ICarServices cars;
        private readonly ICompetitionsServices competitions;
        private readonly ITeamServices teams;
        private readonly IRallyPilotsServices pilots;
        private readonly IRallyNavigatorsServices navigators;
        private readonly IRunwaysServices runways;

        public CompetitionsController(ICarServices carServices, ICompetitionsServices competitionsServices, ITeamServices teamServices,
           IRallyPilotsServices rallyPilots, IRallyNavigatorsServices rallyNavigators, IRunwaysServices runwaysServices)
        {
            this.cars = carServices;
            this.competitions = competitionsServices;
            this.teams = teamServices;
            this.pilots = rallyPilots;
            this.navigators = rallyNavigators;
            this.runways = runwaysServices;
        }


        [HttpGet]
        public IActionResult RallyЕntry()
        {
            var user = User.Identity.Name;
            var team = teams.FindUser(user);
            var viewModel = new TeamViewModels
            {
                Id = team.Id,
                Name = team.Name,
                Cars = cars.GetCar(user),
                StartRaceDate = competitions.GetStartDate().ToString("D"),
                RallyPilots = pilots.GetPilots(user),
                RallyNavigators = navigators.GetNavigators(user),
                Runway = runways.GetRunwayForCurrentRace(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult RallyЕntry(TeamViewModels input)
        {
           

            return this.View();
        }
    }
}
