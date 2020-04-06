namespace BgRallyRace.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using BgRallyRace.Services.Dismissal;

    [Authorize]
    public class DismissalController : Controller
    {
        private readonly IDismissalServices dismissal;

        public DismissalController(IDismissalServices dismissalServices)
        {
            dismissal = dismissalServices;
        }

       
        public IActionResult DismissalPilot(int id)
        {
            dismissal.DismissalPilot(id);
            return this.RedirectToAction("Pilot","Teams");
        }

        public IActionResult DismissalNavigator(int id)
        {
            dismissal.DismissalNavigator(id);
            return this.RedirectToAction("Navigator", "Teams");
        }

        public IActionResult DismissalFitter(int id)
        {
            return this.RedirectToAction("Index", "Home");
        }
    }
}
