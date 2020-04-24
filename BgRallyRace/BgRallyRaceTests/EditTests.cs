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

    public class EditTests
    {
        [Fact]
        public void CorrectEditRunways()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new ApplicationDbContext(options.Options);
            var pilot = new RallyPilotsServices(repository);
            var competition = new CompetitionsServices(repository);
            var runways = new RunwaysServices(repository);
            var creat = new CreateServices(repository);

            var service = new EditServices(repository,runways,pilot,competition);
            var input = new RunwayViewModels
            {
                NameRunway = "Test1111",
                Difficulty = DifficultyType.Average,
                Description = "TestTest1111",
                TrackLength = 123,
                ImagName = "test1111",
            };

            var input1 = new RunwayViewModels
            {
                Id = 1,
                NameRunway = "Test",
                Difficulty = DifficultyType.Average,
                Description = "TestTest",
                TrackLength = 123,
                ImagName = "test",
            };

            creat.CreateRunway(input);
            var result = service.EditRunways(input1).Result;
            var resultRunway = runways.GetRunway(1).Result;

            var resultId = resultRunway.Id;
            var resultImagName = resultRunway.ImagName;
            var resultTrackLength = resultRunway.TrackLength;
            var resultDescription = resultRunway.Description;


            Assert.Equal("TestTest", resultDescription);
            Assert.Equal("test", resultImagName);
            Assert.Equal(1, resultId);
            Assert.Equal(123, resultTrackLength);
            Assert.Equal("Пистата, е променена успешно.", result);
        }

        [Fact]
        public void CorrectEditPilot()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new ApplicationDbContext(options.Options);
            var pilot = new RallyPilotsServices(repository);
            var competition = new CompetitionsServices(repository);
            var runways = new RunwaysServices(repository);
            var creat = new CreateServices(repository);

            var service = new EditServices(repository, runways, pilot, competition);

            var input = new PilotViewModels
            {
                FirstName = "ffdzf",
                LastName = "dzsf",
                Age = 18,
            };

            var input1 = new PilotViewModels
            {
                Id =1,
                FirstName = "Pesho",
                LastName = "Ivanov",
                Age = 30,
            };

            creat.CreatePilot(input);
            var result = service.EditPilot(input1).Result;
            var resultPiot = pilot.GetPilot(1);

            var resultId = resultPiot.Id;
            var resultFirstName = resultPiot.FirstName;
            var resultLastName = resultPiot.LastName;
            var resultAge = resultPiot.Age;


            Assert.Equal("Pesho", resultFirstName);
            Assert.Equal("Ivanov", resultLastName);
            Assert.Equal(1, resultId);
            Assert.Equal(30, resultAge);
            Assert.Equal("Пилота, е променен успешно.", result);
        }

    }
}
