using System.ComponentModel.DataAnnotations;

namespace BgRallyRace.ViewModels
{
    public class PeopleViewModel : PagesViewModels
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [Range(610, 5000)]
        public int Salary { get; set; }

        [Required]
        [Range (18, 100)]
        public int Age { get; set; }

        [Required]
        [Range(0, 100)]
        public int Concentration { get; set; }

        [Required]
        [Range(0, 100)]
        public int Experience { get; set; }

        [Required]
        [Range(0, 100)]
        public int Energy { get; set; }

        [Required]
        [Range(0, 100)]
        public int Devotion { get; set; }

        [Required]
        [Range(0, 100)]
        public int PhysicalTraining { get; set; }

        [Required]
        [Range(60, 150)]
        public int Pounds { get; set; }

        public string Text { get; set; }
    }
}
