﻿namespace BgRallyRace.ViewModels
{
    using BgRallyRace.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class NavigatorViewModels : PeopleViewModel
    {
        [Required]
        [Range(0, 100)]
        public int Communication { get; set; }

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public List<RallyNavigators> Navigators { get; set; } = new List<RallyNavigators>();
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    }
}

