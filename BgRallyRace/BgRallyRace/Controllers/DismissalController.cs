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
        public IActionResult DismissalPilot()
        {
            var pilots = TempData["Pilots"] as int[];
            dismissal.DismissalPilot(pilots[0]);
            return this.RedirectToAction("Pilot","Teams");
        }

        [Authorize]
        public IActionResult DismissalNavigator()
        {
            var navigators = TempData["Navigator"] as int[];
            dismissal.DismissalNavigator(navigators[0]);
            return this.RedirectToAction("Navigator", "Teams");
        }

        [Authorize]
        public IActionResult DismissalFitter()
        {
            return this.RedirectToAction("Index", "Home");
        }
    }
}
