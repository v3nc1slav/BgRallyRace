namespace BgRallyRace.Models.Money
{
    using BgRallyRace.Models.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;
    public class FinancialStatistics
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public FundsType Funds { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public decimal MoneExpense { get; set; }

        public decimal MoneRevenue { get; set; }
    }
}
