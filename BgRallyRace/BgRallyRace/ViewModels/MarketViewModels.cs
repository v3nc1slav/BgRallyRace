namespace BgRallyRace.ViewModels
{
    using BgRallyRace.Models;
    using BgRallyRace.Models.PartsCar;
    using System.Collections.Generic;
    public class MarketViewModels : PagesViewModels
    {
       public  ICollection<RallyPilots> Pilots { get; set; } = new HashSet<RallyPilots>();
       public ICollection<RallyNavigators> Navigators { get; set; } = new HashSet<RallyNavigators>();
       public ICollection<Parts> Parts { get; set; } =  new HashSet<Parts>();

    }
}
