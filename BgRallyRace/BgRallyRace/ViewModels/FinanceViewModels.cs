﻿namespace BgRallyRace.ViewModels
{
    using System.Collections.Generic;
    public class FinanceViewModels
    {
        public decimal[] MoneExpense { get; set; }
       
        public decimal[] MoneRevenue { get; set; }

        public decimal TotalRevenue { get; set; }

        public decimal TotalExpense { get; set; }

        public decimal InitialBalance { get; set; }

        public decimal CurrentBalance { get; set; }
    }
}
