using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Models
{
    public class Teams
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int CompetitionId { get; set; }

        public Competitions Competition { get; set; } 

        [Required]
        public int RallyPilotId { get; set; }

        public ICollection<RallyPilots> RallyPilot { get; set; } = new HashSet<RallyPilots>();

        [Required]
        public int RallyNavigatorId { get; set; }

        public ICollection< RallyNavigators> RallyNavigator { get; set; } = new HashSet<RallyNavigators>();

        [Required]
        public int FitterId { get; set; }

        public ICollection<RallyFitters> Fitter { get; set; } = new HashSet<RallyFitters>();

        [Required]
        public int UserId { get; set; }
        public IdentityUser User { get; set; }

        public int MoneyAccountId { get; set; }

        public MoneyAccount MoneyAccount { get; set; }

        public int CarId { get; set; }

        public Cars Cars { get; set; }
    }
}
