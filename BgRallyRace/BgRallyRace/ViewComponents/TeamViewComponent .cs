namespace BgRallyRace.ViewComponents
{
    using BgRallyRace.Services;
    using BgRallyRace.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [ViewComponent(Name = "Team")]
    public class TeamViewComponent : ViewComponent
    {
        private readonly ITeamServices team;

        public TeamViewComponent(ITeamServices teamServices)
        {
            team = teamServices;
        }

        public ITeamServices TeamServices { get; }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new TeamViewModels();
            viewModel.Team = team.FindUser(User.Identity.Name);
            return View(viewModel);
        }
    }
}
