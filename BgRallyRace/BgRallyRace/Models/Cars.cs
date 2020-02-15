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

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public Turbo? Turbo { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

        public int? TeamId { get; set; }

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public Team? Team { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    }
}