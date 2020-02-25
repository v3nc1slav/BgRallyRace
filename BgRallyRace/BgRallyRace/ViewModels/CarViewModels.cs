namespace BgRallyRace.ViewModels
{
    using BgRallyRace.Models;

    public class CarViewModels
    {
        public int CarId { get; set; }
        public Cars Car { get; set; }
        public Cars Cars { get; set; }
        public Aerodynamics Aerodynamics { get; set; }
        public Brakes Brakes { get; set; }
        public Engines Engines { get; set; }
        public Gearboxs Gearboxs { get; set; }
        public ModelsCars ModelsCars { get; set; }
        public Mountings Mountings { get; set; }
        public Turbo? Turbo { get; set; }
        public decimal MaxSpeed { get; set; }
        public decimal CurrentSpeed { get; set; }

    }
}
