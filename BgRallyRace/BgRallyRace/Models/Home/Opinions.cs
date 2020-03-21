namespace BgRallyRace.Models
{
    using System;
    using BgRallyRace.Models.Enums;

    public class Opinions
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime DateOfPublication { get; set; }

        public int UserId { get; set; }

        public string  User { get; set; }

        public bool IsDeleted { get; set; }

        public AuthorizationType authorizationOpinions { get; set; }

        //public ICollection<string> FullData { get; set; } = new HashSet<string>(); ??
    }
}
