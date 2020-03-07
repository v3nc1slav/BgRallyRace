using BgRallyRace.Models.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Models
{
    public class People
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Salary { get; set; }

        public int Age { get; set; }

        public int Concentration { get; set; }

        public int Experience { get; set; }

        public int Energy { get; set; }

        public int Devotion { get; set; }

        public int PhysicalTraining { get; set; }

        public int Pounds { get; set; }

        public bool IsItWorking { get; set; }
    }
}
