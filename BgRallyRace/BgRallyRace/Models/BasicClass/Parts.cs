namespace BgRallyRace.Models.PartsCar
{
    using System.ComponentModel.DataAnnotations;

    public class Parts
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(100)]
        public decimal Strength { get; set; }

        [Required]
        public decimal Speed { get; set; }

        public int CarId { get; set; }

        public Cars Cars { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
