namespace BgRallyRaceTests
{
    using BgRallyRace.Data;
    using BgRallyRace.Models.Enums;
    using BgRallyRace.Services;
    using BgRallyRace.Services.Admin;
    using BgRallyRace.Services.Competitions;
    using BgRallyRace.Services.Runways;
    using BgRallyRace.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using System;
    using Xunit;

    public class CreateTest
    {
        [Fact]
        public void CorrectCreateCompetitions()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new ApplicationDbContext(options.Options);
            var service = new CreateServices(repository);
            var competition = new CompetitionsServices(repository);
            var date = DateTime.Now;

            var input = new CompetitionsViewModels
            {
                Name = "Test",
                PrizeFund = 150000,
                StartRaceDate = date,
            };

            var result = service.CreateCompetitionsAsync(input);

            var resultName = competition.GetCompetitionName().Result;
            var resultId = competition.GetCompetitionId();
            var resultStartDate = competition.GetStartDate().Result;
            var resultPrize = competition.GetCompetitionPrizeFund();


            Assert.Equal(date, resultStartDate);
            Assert.Equal("Test", resultName);
            Assert.Equal(1, resultId);
            Assert.Equal(150000, resultPrize);
            Assert.Equal("Състезянието е успешно създадено.", result);
        }

        [Fact]
        public void CorrectCreateRunway()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new ApplicationDbContext(options.Options);
            var service = new CreateServices(repository);
            var runway = new RunwaysServices(repository);
            var date = DateTime.Now;

            var input = new RunwayViewModels
            {
                NameRunway = "Test",
                Difficulty = DifficultyType.Average,
                Description = "TestTest",
                TrackLength = 123,
                ImagName = "test",
            };

            var result = service.CreateRunwayAsync(input);
            var resultRunway = runway.GetRunwayAsync(1).Result;

            var resultId = resultRunway.Id;
            var resultImagName = resultRunway.ImagName;
            var resultTrackLength = resultRunway.TrackLength;
            var resultDescription = resultRunway.Description;


            Assert.Equal("TestTest", resultDescription);
            Assert.Equal(null, resultImagName);
            Assert.Equal(1, resultId);
            Assert.Equal(123, resultTrackLength);
            Assert.Equal("Пистата е успешно създадено.", result);
        }

        [Fact]
        public void CorrectCreatePilot()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new ApplicationDbContext(options.Options);
            var service = new CreateServices(repository);
            var pilot = new RallyPilotsServices(repository);

            var input = new PilotViewModels
            {
                FirstName = "Pesho",
                LastName = "Ivanov",
                Age = 30,
            };

            var result = service.CreatePilotAsync(input);
            var resultPiot = pilot.GetPilot(1);

            var resultId = resultPiot.Id;
            var resultFirstName = resultPiot.FirstName;
            var resultLastName = resultPiot.LastName;
            var resultAge = resultPiot.Age;


            Assert.Equal("Pesho", resultFirstName);
            Assert.Equal("Ivanov", resultLastName);
            Assert.Equal(1, resultId);
            Assert.Equal(30, resultAge);
            Assert.Equal("Пилота е успешно създадено.", result);
        }

        [Fact]
        public void CorrectCreateNavigator()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new ApplicationDbContext(options.Options);
            var service = new CreateServices(repository);
            var pilot = new RallyNavigatorsServices(repository);

            var input = new NavigatorViewModels
            {
                FirstName = "Pesho",
                LastName = "Ivanov",
                Energy = 100,
            };

            var result = service.CreateNavigatorAsync(input);
            var resultPiot = pilot.GetNavigator(1);

            var resultId = resultPiot.Id;
            var resultFirstName = resultPiot.FirstName;
            var resultLastName = resultPiot.LastName;
            var resultEnergy = resultPiot.Energy;


            Assert.Equal("Pesho", resultFirstName);
            Assert.Equal("Ivanov", resultLastName);
            Assert.Equal(1, resultId);
            Assert.Equal(100, resultEnergy);
            Assert.Equal("Навигатора е успешно създадено.", result);
        }

        [Fact]
        public void CorrectCreateParts()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new ApplicationDbContext(options.Options);
            var service = new CreateServices(repository);
            var car = new CarServices(repository);

            var input = new PartsViewModels
            {
                Type =  PartsCarsType.Aerodynamics,
                Name = "Aerodynamics",
                Price = 1000,
                Strength = 100,
                Speed = 10,
            };

            var result = service.CreateParts(input);

            Assert.Equal("Часта е успешно създадено.", result);
        }
    }
}
