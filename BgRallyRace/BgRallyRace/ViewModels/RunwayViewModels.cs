namespace BgRallyRace.ViewModels
{
    using BgRallyRace.Models.Enums;

    public class RunwayViewModels
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal TrackLength { get; set; }

        public DifficultyType Difficulty { get; set; }

        public string Description { get; set; }
    }
}
