using System.ComponentModel.DataAnnotations;

namespace BgRallyRace.Models
{
    public class Cars
    {
        [Key]
        public int Id { get; set; }

        public ModelsCars ModelCar { get; set; } = new ModelsCars();

        public Engines Engine { get; set; } = new Engines();

        public Mountings Mounting { get; set; } = new Mountings();

        public Brakes Brakes { get; set; } = new Brakes();

        public Gearboxs Gearbox { get; set; } = new Gearboxs();

        public Aerodynamics Aerodynamics { get; set; } = new Aerodynamics();

        public Turbo Turbo { get; set; } = new Turbo();

        public int TeamId { get; set; }

        public Team Team { get; set; }
    }
}