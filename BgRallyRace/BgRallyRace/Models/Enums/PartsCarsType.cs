using System.ComponentModel.DataAnnotations;

namespace BgRallyRace.Models.Enums
{
    public enum PartsCarsType
    {
        [Display (Name = "Аеродинамика")]
        Aerodynamics = 0,

        [Display(Name = "Спирачки")]
        Brakes = 1,

        [Display(Name = "Двигатели")]
        Engines = 2,

        [Display(Name = "Скоростни кутии")]
        GearBoxs = 3,

        [Display(Name = "Купе")]
        ModelsCars = 4,

        [Display(Name = "Шаси")]
        Mountings = 5,

        [Display(Name = "Турбо")]
        Turbo = 6,
    }
}
