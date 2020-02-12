using System.ComponentModel.DataAnnotations;

namespace BgRallyRace.Models
{
    public class Cars
    {
        [Key]
        public int Id { get; set; }

        public ModelsCars ModelCar { get; set; } 

        public Engines Engine { get; set; }

        public Mountings Mounting { get; set; } 

        public Brakes Brakes { get; set; } 

        public Gearboxs Gearbox { get; set; } 

        public Aerodynamics Aerodynamics { get; set; } 

        public Turbo? Turbo { get; set; } 

        public int? TeamId { get; set; }

        public Team? Team { get; set; }
    }
}