using System.ComponentModel.DataAnnotations;

namespace BgRallyRace.Models
{
    public class Cars
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public ModelsCars ModelCar { get; set; }
        [Required]
        public Engines Engine { get; set; }
        [Required]
        public Mountings Mounting { get; set; }
        [Required]
        public Brakes Brakes { get; set; }
        [Required]
        public Gearboxs Gearbox { get; set; }
        [Required]
        public Aerodynamics Aerodynamics { get; set; }
        public Turbo? Turbo { get; set; } 
   }
}