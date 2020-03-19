namespace BgRallyRace.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using BgRallyRace.Services;
    using BgRallyRace.ViewModels;
    using BgRallyRace.Models.PartsCar;
    using BgRallyRace.Services.Dismissal;

    public class DismissalController : Controller
    {
        private readonly IDismissalServices dismissal;

        public DismissalController(IDismissalServices dismissalServices)
        {
            dismissal = dismissalServices;
        }

        [Authorize]
        public IActionResult DismissalPilot(int id)
        {
            dismissal.DismissalPilot(id);
            return this.RedirectToAction("Pilot","Teams");
        }

        [Authorize]
        public IActionResult DismissalNavigator(int id)
        {
            dismissal.DismissalNavigator(id);
            return this.RedirectToAction("Navigator", "Teams");
        }

        [Authorize]
        public IActionResult DismissalFitter(int id)
        {
            return this.RedirectToAction("Index", "Home");
        }
    }
}
