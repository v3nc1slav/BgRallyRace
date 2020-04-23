namespace BgRallyRace.ViewModels
{
    using BgRallyRace.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    public class PartsViewModels
    {
        public PartsCarsType Type { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 10000)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 100)]
        public decimal Strength { get; set; }

        [Required]
        [Range(1, 10000)]
        public decimal Speed { get; set; }

        public string Text { get; set; }

    }
}
