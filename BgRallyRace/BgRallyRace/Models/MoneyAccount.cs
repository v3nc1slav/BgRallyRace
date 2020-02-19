using System.ComponentModel.DataAnnotations;

namespace BgRallyRace.Models
{
    public class MoneyAccount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Balance { get; set; }

        public int UserId { get; set; }
        public string User { get; set; }
    }
}