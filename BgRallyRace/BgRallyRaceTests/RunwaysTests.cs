namespace BgRallyRaceTests
{
    using BgRallyRace.Data;
    using BgRallyRace.Models.Competitions;
    using BgRallyRace.Models.Enums;
    using BgRallyRace.Services.Admin;
    using BgRallyRace.Services.Runways;
    using BgRallyRace.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using System;
    using Xunit;

    public  class RunwaysTests
    {
        //InMemoryTest
        [Fact]
        public void CorrectGetAllRunways()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new ApplicationDbContext(options.Options);
            var creat = new CreateServices(repository);

            var service = new RunwaysServices(repository);

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

            creat.CreateRunwayAsync(input);
            creat.CreateRunwayAsync(input1);
            var resultRunway = service.GetAllRunwaysAsync();

            var resultId = resultRunway[0].Id;
            var resultImagName = resultRunway[0].ImagName;
            var resultTrackLength = resultRunway[0].TrackLength;
            var resultDescription = resultRunway[0].Description;
            var resultId1 = resultRunway[1].Id;
            var resultImagName1 = resultRunway[1].ImagName;
            var resultTrackLength1 = resultRunway[1].TrackLength;
            var resultDescription1 = resultRunway[1].Description;
            var count = resultRunway.Count;

            Assert.Equal("TestTest1111", resultDescription);
            Assert.Equal(null, resultImagName);
            Assert.Equal(1, resultId);
            Assert.Equal(123, resultTrackLength);
            Assert.Equal("TestTest", resultDescription1);
            Assert.Equal(null, resultImagName1);
            Assert.Equal(2, resultId1);
            Assert.Equal(123, resultTrackLength1);
            Assert.Equal(2, count);
        }

      
        //MockTests
        [Fact]
        public void CorrectGetRunwayForRace()
        {
            var cars = new Mock<IRunwaysServices>();
            var car = new RallyRunway
            {
                Id = 1,
                Name = "Test",
                Difficulty = DifficultyType.Average,
                TrackLength = 123,
                ImagName = "test",
            };

             cars.Setup(x => x.GetRunwayForCurrentRace())
                .Returns(car);

            var result = cars.Object.GetRunwayForCurrentRace();

            var resultName = result.Name;

            Assert.Equal("Test", resultName);

            cars.Verify(x => x.GetRunwayForCurrentRace(), Times.Once);
        }

    }
}
