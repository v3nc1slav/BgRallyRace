namespace BgRallyRace.Services.Competitions
{
    using BgRallyRace.Models;
    using System;
    using System.Collections.Generic;

    public interface IRatingListServices
    {
        void AddInRatingList(Team team, DateTime time);

        void AddPonts();

        void AddPontsSE();

        Dictionary<Team, DateTime> GetRatingList();

        List<Team> DistributionPoint();

    }
}
