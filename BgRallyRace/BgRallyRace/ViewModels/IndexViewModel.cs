namespace BgRallyRace.Models.Home
{
    using System;
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<string> UserNames { get; set; }

        public int CountNotAuthorization { get; set; }

        public DateTime StartDate { get; set; }
    }
}
