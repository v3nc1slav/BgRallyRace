using BgRallyRace.Data;
using BgRallyRace.Models;
using BgRallyRace.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BgRallyRaceTests
{
    public class RallyPilotsTests
    {

        //InMemoryTest
        [Fact]
        public void CorrectGetNewAerodynamics()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new ApplicationDbContext(options.Options);
            var pilots = new RallyPilotsServices(repository);

            pilots.CreateRallyPilotsAsync();

            var pilot = pilots.GetPilot(1);

            var resultAge = pilot.Age;

            Assert.Equal(18, resultAge);
        }
    }
}
