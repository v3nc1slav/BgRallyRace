namespace BgRallyRace.ViewModels
{
    using BgRallyRace.Models;
    using System.ComponentModel.DataAnnotations;

    public class OpinionsViewModels : PagesViewModels
    {
        [MinLength (3), MaxLength(250)]
        [Required]
        public string Opinion { get; set; }

        public Opinions[] Opinions { get; set; } 

        public Opinions[] OpinionsForAdmin { get; set; }

        public int CountNotAuthorization { get; set; }

        public string Text { get; set; }
     
    }
}
