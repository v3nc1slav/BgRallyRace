namespace BgRallyRace.ViewModels
{
    using BgRallyRace.Models;
    using BgRallyRace.Models.Enums;
    using System.Collections.Generic;

    public class TeamViewModels : PagesViewModels
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Cars Cars { get; set; }

        public int CarId { get; set; }

        public string StartRaceDate { get; set; }

        public int RallyPilotId { get; set; }

        public List<RallyPilots> RallyPilots { get; set; }

        public int RallyNavigatorId { get; set; }

        public List<RallyNavigators> RallyNavigators { get; set; }

        public int FitterId { get; set; }

        public RallyFitters RallyFitter { get; set; }

        public Team Team { get; set; }

        public int TeamId { get; set; }

        public RallyRunway Runway { get; set; }

        public DriveType Drive { get; set; }

        public string BGDrive {
            get {

                if (Drive == DriveType.Aggressive)
                {
                    return "Агресивно";
                }
                else if (Drive == DriveType.Normal)
                {
                    return "Нормално";
                }
                else if (Drive == DriveType.Caution)
                {
                    return "Предпазливо";
                }
                return "Няма Турбо";
            }
        }
  
        public UseOfTurboType UseOfTurbo { get; set; }

        public int CompetitionId { get; set; }

        public string CompetitionName { get; set; }
    }
}
