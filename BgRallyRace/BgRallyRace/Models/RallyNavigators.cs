using System.ComponentModel.DataAnnotations;

namespace BgRallyRace.Models
{
    public class RallyNavigators
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Teams Teams { get; set; }
    }
}