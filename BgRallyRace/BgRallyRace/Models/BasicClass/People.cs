using System.ComponentModel.DataAnnotations;

namespace BgRallyRace.Models
{
    public class People
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [MinLength(610)]
        public int Salary { get; set; }

        [Required]
        [MinLength(18), MaxLength(100)]
        public int Age { get; set; }

        [Required]
        [MinLength(0), MaxLength(100)]
        public int Concentration { get; set; }

        [Required]
        [MinLength(0), MaxLength(100)]
        public int Experience { get; set; }

        [Required]
        [MinLength(0), MaxLength(100)]
        public int Energy { get; set; }

        [Required]
        [MinLength(0), MaxLength(100)]
        public int Devotion { get; set; }

        [Required]
        [MinLength(0), MaxLength(100)]
        public int PhysicalTraining { get; set; }

        [Required]
        [MinLength(60), MaxLength(150)]
        public int Pounds { get; set; }

        public bool IsItWorking { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
