namespace BgRallyRace.Controllers
{
    using BgRallyRace.Services;
    using BgRallyRace.Services.Competitions;
    using BgRallyRace.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CompetitionsController : Controller
    {
        private readonly ICarServices cars;
        private readonly ICompetitionsServices competitions;
        private readonly ITeamServices teams;

        public CompetitionsController(ICarServices carServices, ICompetitionsServices competitionsServices, ITeamServices teamServices)
        {
            this.cars = carServices;
            this.competitions = competitionsServices;
            this.teams = teamServices;
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
                Cars = team.Cars,
                RallyPilot = team.RallyPilot,
                RallyNavigator = team.RallyNavigator,
            };

            return this.View(viewModel);
        }
    }
}
