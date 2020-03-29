﻿namespace BgRallyRace.ViewModels
{
    using BgRallyRace.Models;
    using System.Collections.Generic;

    public class PilotViewModels : PeopleViewModel
    {
        public int Reflexes { get; set; }

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public List<RallyPilots> Pilots { get; set; } = new List<RallyPilots>();
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    }
}
