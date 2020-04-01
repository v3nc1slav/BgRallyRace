namespace BgRallyRace.ViewModels
{
    using BgRallyRace.Models;
    using System.Collections.Generic;
    public class MarketViewModels : PagesViewModels
    {
       public  ICollection<RallyPilots> Pilots { get; set; } = new HashSet<RallyPilots>();
       public ICollection<RallyNavigators> Navigators { get; set; } = new HashSet<RallyNavigators>();
       public ICollection<PartsCars> Parts { get; set; } =  new HashSet<PartsCars>();

    }
}
