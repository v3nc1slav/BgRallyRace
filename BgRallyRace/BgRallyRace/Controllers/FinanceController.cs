namespace BgRallyRace.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using BgRallyRace.ViewModels;
    using BgRallyRace.Services.Money;

    [Authorize]
    public class FinanceController : Controller
    {
        private readonly IFinanceServices finance;

        public FinanceController(IFinanceServices financeServices)
        {
            this.finance = financeServices;
        }

        public IActionResult FinancialStatistics()
        {
            var viewModel = new FinanceViewModels
            {
                MoneExpense = finance.GetExpense(User.Identity.Name),
                MoneRevenue = finance.GetRevenue(User.Identity.Name),
                TotalExpense = finance.GetTotalExpense(User.Identity.Name),
                TotalRevenue = finance.GetTotalRevenue(User.Identity.Name),
            };
            return this.View(viewModel);
        }
    }
}
