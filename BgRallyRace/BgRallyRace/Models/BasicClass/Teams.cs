using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Models.Teams
{
    public class Teams
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
  
        public int UserId { get; set; }
        public string User { get; set; }

        [Required]
        public int MoneyAccountId { get; set; }

        public MoneyAccount MoneyAccount { get; set; }

        [Required]
        public int CarId { get; set; }

        public Cars Cars { get; set; }
    }
}
