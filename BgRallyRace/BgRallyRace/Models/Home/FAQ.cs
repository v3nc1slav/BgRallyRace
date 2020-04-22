namespace BgRallyRace.Models.Home
{
    using BgRallyRace.Models.Enums;
    using System;
    public class FAQ
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public DateTime DateOfPublication { get; set; }

        public int UserId { get; set; }

        public string User { get; set; }

        public bool IsDeleted { get; set; }

        public AuthorizationType authorizationOpinions { get; set; } = AuthorizationType.indefinitely;

    }
}
