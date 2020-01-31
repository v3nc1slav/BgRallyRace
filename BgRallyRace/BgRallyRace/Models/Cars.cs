using System.ComponentModel.DataAnnotations;

namespace BgRallyRace.Models
{
    public class Cars
    {
        [Key]
        public int Id { get; set; }

        public string Type { get; set; }

    }
}