namespace BgRallyRace.Models.Competitions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RatingList
    {
        public int Id { get; set; }

        public List<Team> TeamRankings { get; set; } = new List<Team>();

        public string NameCompetitions { get; set; }

        public DateTime DateCompetitions { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
