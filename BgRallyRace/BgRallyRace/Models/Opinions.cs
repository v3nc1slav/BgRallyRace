namespace BgRallyRace.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;
    public class Opinions
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime DateOfPublication { get; set; }

        public int UserId { get; set; }
        public IdentityUser  User { get; set; }

        //public ICollection<string> FullData { get; set; } = new HashSet<string>(); ??
    }
}
