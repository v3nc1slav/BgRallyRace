namespace BgRallyRace.Models
{
    using System;
    using Microsoft.AspNetCore.Identity;
    public class Opinions
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime DateOfPublication { get; set; }

        public int UserId { get; set; }
        public IdentityUser  User { get; set; }
    }
}
