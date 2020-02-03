using System.ComponentModel.DataAnnotations;

namespace BgRallyRace.Models
{
    public class Cars
    {
        [Key]
        public int Id { get; set; }

        public string ModelsCarId { get; set; }

        public ModelsCars ModelCar { get; set; }

        public int EngineId { get; set; }

        public Engines Engine { get; set; }

        public int MountingId { get; set; }

        public Mountings Mounting { get; set; }

        public int BrakesId { get; set; }

        public Brakes Brakes { get; set; }

        public int GearboxId { get; set; }

        public Gearboxs Gearbox { get; set; }

        public int AerodynamicsId { get; set; }

        public Aerodynamics Aerodynamics { get; set; }

        public int TurboId { get; set; }

        public Turbo Turbo { get; set; }
    }
}