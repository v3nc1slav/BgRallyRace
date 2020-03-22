namespace BgRallyRace.Services
{
    using BgRallyRace.Models;

    public interface IMoneyAccountServices
    {
        void CreateMoneyAccount(string user);

         void ExpenseAccountAsync(decimal expense, string user);

         void RevenueAccountAsync(decimal revenue, string user);

         MoneyAccount? FindUserAsync(string user);

        int FindIdMoneyAccountAsync(string user);

        decimal GetBalanceAsync(string user);

    }
}
