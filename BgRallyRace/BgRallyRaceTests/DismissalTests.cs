namespace BgRallyRaceTests
{
    using BgRallyRace.Data;
    using BgRallyRace.Services;
    using Microsoft.EntityFrameworkCore;
    using System;
    using Xunit;

    public class DismissalTests
    {
        //InMemoryTest
        [Fact]
        public void CorrectRentalsPilot()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new ApplicationDbContext(options.Options);
            var money = new MoneyAccountServices(repository);
            var pilot = new RallyPilotsServices(repository);
            var navigator = new RallyNavigatorsServices(repository);
            var car = new CarServices(repository, pilot, navigator, money);
            var team = new TeamServices(repository, car, money, pilot, navigator);

            money.CreateMoneyAccount("Pesho");
            team.CreateTeamAsync("Vidin", "Pesho").GetAwaiter().GetResult();

            var t = team.FindUserAsync("Pesho").Result;
            var id = t.RallyPilotId;

        }
    }
}
