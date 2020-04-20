namespace BgRallyRace.Models.Home
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class IndexViewModel
    {
        public IEnumerable<string> UserNames { get; set; }

        public int CountNotAuthorization { get; set; }

        public DateTime StartDate { get; set; }

        public Team Team { get; set; }

        public string StartDateNow { get { return  this.StartDate.AddHours(12).ToString("MMMM dd, yyyy hh:mm:ss.F", CultureInfo.CreateSpecificCulture("en-US")); } }
    }
}
