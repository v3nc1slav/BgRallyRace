namespace BgRallyRace.ViewModels
{
    using BgRallyRace.Models.Home;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class FAQViewModels
    {
        public string Question { get; set; }

        public string Answer { get; set; }

        public List<FAQ> FAQs { get; set; }
    }
}
