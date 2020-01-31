using System.ComponentModel.DataAnnotations;

namespace BgRallyRace.Models
{
    public class MoneyAccount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Balance { get; set; }
    }
}