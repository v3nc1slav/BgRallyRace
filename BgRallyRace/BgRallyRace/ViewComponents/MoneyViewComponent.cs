namespace BgRallyRace.ViewComponents
{
    using BgRallyRace.Services;
    using BgRallyRace.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [ViewComponent(Name = "Money")]
    public class MoneyViewComponent : ViewComponent
    {
        private readonly IMoneyAccountServices moneyAccount;

        public MoneyViewComponent(IMoneyAccountServices moneyAccount)
        {
            this.moneyAccount = moneyAccount;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new MoneyAccountViewModels();
            viewModel.Balance = moneyAccount.GetBalanceAsync(User.Identity.Name);
            return View(viewModel);
        }
    }
}
