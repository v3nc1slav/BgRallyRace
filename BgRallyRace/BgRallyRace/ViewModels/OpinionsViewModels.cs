namespace BgRallyRace.ViewModels
{
    using BgRallyRace.Models;

    public class OpinionsViewModels : PagesViewModels
    {
        public Opinions[] Opinions { get; set; } 

        public Opinions[] OpinionsForAdmin { get; set; }

        public int CountNotAuthorization { get; set; }

    }
}
