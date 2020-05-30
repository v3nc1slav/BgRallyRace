namespace BgRallyRace.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using BgRallyRace.ViewModels;
    using BgRallyRace.Services.Money;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;
    using BgRallyRace.Models;
    using System.Diagnostics;

    [Authorize]
    public class FinanceController : Controller
    {
        private readonly ILogger<FinanceController> _logger;
        private readonly IFinanceServices finance;

        public FinanceController(ILogger<FinanceController> logger, IFinanceServices financeServices)
        {
            this._logger = logger;
            this.finance = financeServices;
        }

        [HttpGet]
        public async Task<IActionResult> FinancialStatistics()
        {
            _logger.LogInformation("view financial statistics");
            var viewModel = new FinanceViewModels
            {
                MoneExpense = finance.GetExpense(User.Identity.Name).ToArray(),
                MoneRevenue = finance.GetRevenue(User.Identity.Name).ToArray(),
                TotalExpense = finance.GetTotalExpense(User.Identity.Name),
                TotalRevenue = finance.GetTotalRevenue(User.Identity.Name),
            };
            return this.View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
