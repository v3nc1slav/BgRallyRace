using BgRallyRace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Services
{
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
