namespace BgRallyRace.Models
{
    using BgRallyRace.Models.Enums;
    using BgRallyRace.Models.PartsCar;
    using System.ComponentModel.DataAnnotations;

    public class PartsCars : Parts
    {
        [Required]
        public PartsCarsType Type { get; set; }
    }
}
