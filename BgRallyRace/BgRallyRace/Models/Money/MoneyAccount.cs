namespace BgRallyRace.Models
{
    using BgRallyRace.Models.Money;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class MoneyAccount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Balance { get; set; }

        public int FinancialStatisticsId { get; set; }
        public ICollection<FinancialStatistics>? FinancialStatistics { get; set; } = new List<FinancialStatistics>();

        public int UserId { get; set; }
        public string User { get; set; }

    }
}