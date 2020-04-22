namespace BgRallyRace.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Logging;
    using BgRallyRace.Models;
    using BgRallyRace.Services.Dismissal;

    [Authorize]
    public class DismissalController : Controller
    {
        private readonly IDismissalServices dismissal;
        private readonly ILogger<DismissalController> _logger;
        public DismissalController(ILogger<DismissalController> logger, IDismissalServices dismissalServices)
        {
            this._logger = logger;
            dismissal = dismissalServices;
       }

        [HttpGet]
        public async Task<IActionResult> DismissalPilot(int id)
        {
            _logger.LogInformation("Dismissal Pilot");
            dismissal.DismissalPilot(id);
            return this.RedirectToAction("Pilot","Teams");
        }

        [HttpGet]
        public async Task<IActionResult> DismissalNavigator(int id)
        {
            _logger.LogInformation("Dismissal Navigator");
            dismissal.DismissalNavigator(id);
            return this.RedirectToAction("Navigator", "Teams");
        }

        [HttpGet]
        public async Task<IActionResult> DismissalFitter(int id)
        {
            _logger.LogInformation("Dismissal Fitter");
            return this.RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
