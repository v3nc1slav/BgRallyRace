namespace BgRallyRaceTests
{
    using BgRallyRace.Data;
    using BgRallyRace.Models.Enums;
    using BgRallyRace.Services;
    using BgRallyRace.Services.Admin;
    using BgRallyRace.Services.Runways;
    using BgRallyRace.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using System;
    using Xunit;

    public class DeleteTests
    {
        [Fact]
        public void CorrectDeleteRunways()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new ApplicationDbContext(options.Options);
            var pilot = new RallyPilotsServices(repository);
            var runways = new RunwaysServices(repository);
            var creat = new CreateServices(repository);

            var service = new DeleteServices(repository, runways, pilot);

            var input = new RunwayViewModels
            {
                NameRunway = "Test",
                Difficulty = DifficultyType.Average,
                Description = "TestTest",
                TrackLength = 123,
                ImagName = "test",
            };

            creat.CreateRunwayAsync(input);
            var result = service.DeleteRunways(1).Result;

            Assert.Equal("Пистата, е изтрита успешно.", result);
        }

        [Fact]
        public void CorrectDeletePilots()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new ApplicationDbContext(options.Options);
            var pilot = new RallyPilotsServices(repository);
            var runways = new RunwaysServices(repository);
            var creat = new CreateServices(repository);

            var service = new DeleteServices(repository, runways, pilot);

            var input = new PilotViewModels
            {
                Id = 1,
                FirstName = "Pesho",
                LastName = "Ivanov",
                Age = 30,
            };

            creat.CreatePilotAsync(input);
            var result = service.DeletePilots(1).Result;
            var resultPiot = pilot.GetPilot(1);

            var resultIsDeleted = resultPiot.IsDeleted;


            Assert.Equal(true, resultIsDeleted);
            Assert.Equal("Пилота, е изтрит успешно.", result);
        }
    }
}
