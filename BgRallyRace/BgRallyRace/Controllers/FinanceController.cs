namespace BgRallyRace.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using BgRallyRace.ViewModels;
    using BgRallyRace.Services.Money;

    public class FinanceController : Controller
    {
        private readonly IFinanceServices finance;

        public FinanceController(IFinanceServices financeServices)
        {
            this.finance = financeServices;
        }

        [Authorize]
        public IActionResult FinancialStatistics()
        {
            var viewModel = new FinanceViewModels
            {
                MoneExpense = finance.GetExpense(User.Identity.Name),
                MoneRevenue = finance.GetRevenue(User.Identity.Name),
            };
            return this.View(viewModel);
        }
    }
}
