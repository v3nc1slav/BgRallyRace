namespace BgRallyRace.Controllers
{
    using BgRallyRace.Services;
    using BgRallyRace.Services.Competitions;
    using BgRallyRace.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CompetitionsController
    {
        private readonly ICarServices cars;
        private readonly ICompetitionsServices competitions;

        public CompetitionsController(ICarServices carServices, ICompetitionsServices competitionsServices)
        {
            this.cars = carServices;
            this.competitions = competitionsServices;
        }


    }
}
