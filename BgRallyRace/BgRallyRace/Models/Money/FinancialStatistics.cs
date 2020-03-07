namespace BgRallyRace.Models.Money
{
    using BgRallyRace.Models.Enums;
    using System.ComponentModel.DataAnnotations;
    public class FinancialStatistics
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public FundsType Funds { get; set; }

        [Required]
        public decimal Mone { get; set; }
    }
}
