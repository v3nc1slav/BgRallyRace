namespace BgRallyRace.ViewModels
{
    using BgRallyRace.Models.Enums;
    using BgRallyRace.Models.Money;
    using System.Collections.Generic;
    public class FinanceViewModels
    {
        public List<FundsType> Funds { get; set; }

        public decimal Money { get; set; }

        public decimal InitialBalance { get; set; }

        public decimal CurrentBalance { get; set; }
    }
}
