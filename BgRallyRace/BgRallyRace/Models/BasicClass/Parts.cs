using System.ComponentModel.DataAnnotations;

namespace BgRallyRace.Models.PartsCar
{
    public class Parts
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal Strength { get; set; }

        public decimal Speed { get; set; }

        public int CarId { get; set; }

        public Cars Cars { get; set; }
    }
}
