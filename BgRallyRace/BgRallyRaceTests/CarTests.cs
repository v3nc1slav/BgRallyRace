namespace BgRallyRaceTests
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using BgRallyRace.Services;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class CarTests
    {

        //MockTests
        [Fact]
        public void CorrectGetCarId()
        {
            var pilots = new Mock<ICarServices>();
            pilots.Setup(x => x.GetCarId(It.IsAny<string>()))
                .Returns(2);

            var result = pilots.Object.GetCarId("Pesho");

            Assert.Equal(2, result);

            pilots.Verify(x => x.GetCarId("Pesho"), Times.Once);
        }

        [Fact]
        public void CorrectGetCars()
        {
            var pilots = new Mock<ICarServices>();
            var car = new Cars
            {
                EngineId = 1,
                GearboxId = 1,
                TeamId = 2,
                AerodynamicsId = 1,
                BrakesId = 1,
                ModelCarId = 1,
                MountingId = 1,
                TurboId = null,
                Id = 1
            };

            pilots.Setup(x => x.GetCars(It.IsAny<string>()))
                .Returns(new List<Cars> 
                { 
                    car
                });

            var result = pilots.Object.GetCars("Pesho");
            var resultEnginId = result[0].EngineId;
            var resultTurboId = result[0].TurboId;
            var resultMountingId = result[0].MountingId;
            var resultId = result[0].Id;

            Assert.Equal(1, resultId);
            Assert.Equal(1, resultMountingId);
            Assert.Equal(null , resultTurboId) ;
            Assert.Equal(1, resultEnginId);

            pilots.Verify(x => x.GetCars("Pesho"), Times.Once);
        }

        [Fact]
        public void CorrectGetCarByString()
        {
            var pilots = new Mock<ICarServices>();
            var car = new Cars
            {
                EngineId = 1,
                GearboxId = 1,
                TeamId = 2,
                AerodynamicsId = 1,
                BrakesId = 1,
                ModelCarId = 1,
                MountingId = 1,
                TurboId = null,
                Id = 1
            };

            pilots.Setup(x => x.GetCar(It.IsAny<string>()))
                .Returns(car);

            var result = pilots.Object.GetCar("Pesho");
            var resultEnginId = result.EngineId;
            var resultTurboId = result.TurboId;
            var resultMountingId = result.MountingId;
            var resultId = result.Id;

            Assert.Equal(1, resultId);
            Assert.Equal(1, resultMountingId);
            Assert.Equal(null, resultTurboId);
            Assert.Equal(1, resultEnginId);

            pilots.Verify(x => x.GetCar("Pesho"), Times.Once);
        }

        [Fact]
        public void CorrectGetCarById()
        {
            var pilots = new Mock<ICarServices>();
            var car = new Cars
            {
                EngineId = 1,
                GearboxId = 1,
                TeamId = 2,
                AerodynamicsId = 1,
                BrakesId = 1,
                ModelCarId = 1,
                MountingId = 1,
                TurboId = null,
                Id = 1
            };

            pilots.Setup(x => x.GetCar(It.IsAny<int>()))
                .Returns(car);

            var result = pilots.Object.GetCar(1);
            var resultEnginId = result.EngineId;
            var resultTurboId = result.TurboId;
            var resultMountingId = result.MountingId;
            var resultId = result.Id;

            Assert.Equal(1, resultId);
            Assert.Equal(1, resultMountingId);
            Assert.Equal(null, resultTurboId);
            Assert.Equal(1, resultEnginId);

        }

        [Theory]
        [InlineData("Pesho")]
        [InlineData("Ivo")]
        public void CorrectGetAerodynamicsByString(string input)
        {
            var pilots = new Mock<ICarServices>();
            var parts = new Aerodynamics
            {
                Name = "Aerodynamics",
                Price = 100,
                Speed = 40,
                Strength = 50,
                IsDeleted = false,
            };

            pilots.Setup(x => x.GetAerodynamics(It.IsAny<string>()))
                .Returns(parts);

            var result = pilots.Object.GetAerodynamics(input);
            var resultName = result.Name;
            var resultPrice = result.Price;
            var resultSpeed = result.Speed;
            var resultId = result.IsDeleted;

            Assert.Equal(false, resultId);
            Assert.Equal(40, resultSpeed);
            Assert.Equal(100, resultPrice);
            Assert.Equal("Aerodynamics", resultName);

        }

        [Theory]
        [InlineData("Pesho")]
        [InlineData("Ivo")]
        public void CorrectGetBrakesByString(string input)
        {
            var pilots = new Mock<ICarServices>();
            var parts = new Brakes
            {
                Name = "Brakes",
                Price = 100,
                Speed = 40,
                Strength = 50,
                IsDeleted = false,
            };

            pilots.Setup(x => x.GetBrakes(It.IsAny<string>()))
                .Returns(parts);

            var result = pilots.Object.GetBrakes(input);
            var resultName = result.Name;
            var resultPrice = result.Price;
            var resultSpeed = result.Speed;
            var resultId = result.IsDeleted;

            Assert.Equal(false, resultId);
            Assert.Equal(40, resultSpeed);
            Assert.Equal(100, resultPrice);
            Assert.Equal("Brakes", resultName);

        }

        [Theory]
        [InlineData("Pesho")]
        [InlineData("Ivo")]
        public void CorrectGetEngineByString(string input)
        {
            var pilots = new Mock<ICarServices>();
            var parts = new Engines
            {
                Name = "Engine",
                Price = 100,
                Speed = 40,
                Strength = 50,
                IsDeleted = false,
            };

            pilots.Setup(x => x.GetEngine(It.IsAny<string>()))
                .Returns(parts);

            var result = pilots.Object.GetEngine(input);
            var resultName = result.Name;
            var resultPrice = result.Price;
            var resultSpeed = result.Speed;
            var resultId = result.IsDeleted;

            Assert.Equal(false, resultId);
            Assert.Equal(40, resultSpeed);
            Assert.Equal(100, resultPrice);
            Assert.Equal("Engine", resultName);

        }

        [Theory]
        [InlineData("Pesho")]
        [InlineData("Ivo")]
        public void CorrectGetGearboxsByString(string input)
        {
            var pilots = new Mock<ICarServices>();
            var parts = new Gearboxs
            {
                Name = "Gearboxs",
                Price = 100,
                Speed = 40,
                Strength = 50,
                IsDeleted = false,
            };

            pilots.Setup(x => x.GetGearboxs(It.IsAny<string>()))
                .Returns(parts);

            var result = pilots.Object.GetGearboxs(input);
            var resultName = result.Name;
            var resultPrice = result.Price;
            var resultSpeed = result.Speed;
            var resultId = result.IsDeleted;

            Assert.Equal(false, resultId);
            Assert.Equal(40, resultSpeed);
            Assert.Equal(100, resultPrice);
            Assert.Equal("Gearboxs", resultName);

        }

        [Theory]
        [InlineData("Pesho")]
        [InlineData("Ivo")]
        public void CorrectGetModelsCarsByString(string input)
        {
            var pilots = new Mock<ICarServices>();
            var parts = new ModelsCars
            {
                Name = "ModelsCars",
                Price = 100,
                Speed = 40,
                Strength = 50,
                IsDeleted = false,
            };

            pilots.Setup(x => x.GetModelsCars(It.IsAny<string>()))
                .Returns(parts);

            var result = pilots.Object.GetModelsCars(input);
            var resultName = result.Name;
            var resultPrice = result.Price;
            var resultSpeed = result.Speed;
            var resultId = result.IsDeleted;

            Assert.Equal(false, resultId);
            Assert.Equal(40, resultSpeed);
            Assert.Equal(100, resultPrice);
            Assert.Equal("ModelsCars", resultName);

        }

        [Theory]
        [InlineData("Pesho")]
        [InlineData("Ivo")]
        public void CorrectGetMountingsByString(string input)
        {
            var pilots = new Mock<ICarServices>();
            var parts = new Mountings
            {
                Name = "Mountings",
                Price = 100,
                Speed = 40,
                Strength = 50,
                IsDeleted = false,
            };

            pilots.Setup(x => x.GetMountings(It.IsAny<string>()))
                .Returns(parts);

            var result = pilots.Object.GetMountings(input);
            var resultName = result.Name;
            var resultPrice = result.Price;
            var resultSpeed = result.Speed;
            var resultId = result.IsDeleted;

            Assert.Equal(false, resultId);
            Assert.Equal(40, resultSpeed);
            Assert.Equal(100, resultPrice);
            Assert.Equal("Mountings", resultName);

        }

        [Theory]
        [InlineData("Pesho")]
        [InlineData("Ivo")]
        public void CorrectGetTurboString(string input)
        {
            var pilots = new Mock<ICarServices>();
            var parts = new Turbo
            {
                Name = "Turbo",
                Price = 100,
                Speed = 40,
                Strength = 50,
                IsDeleted = false,
            };

            pilots.Setup(x => x.GetTurbo(It.IsAny<string>()))
                .Returns(parts);

            var result = pilots.Object.GetTurbo(input);
            var resultName = result.Name;
            var resultPrice = result.Price;
            var resultSpeed = result.Speed;
            var resultId = result.IsDeleted;

            Assert.Equal(false, resultId);
            Assert.Equal(40, resultSpeed);
            Assert.Equal(100, resultPrice);
            Assert.Equal("Turbo", resultName);

        }

        [Fact]
        public void CorrectGetTurboStringOnNull()
        {
            var pilots = new Mock<ICarServices>();
            var parts = new Turbo
            {
                Name = "Turbo",
                Price = 100,
                Speed = 40,
                Strength = 50,
                IsDeleted = false,
            };

            pilots.Setup(x => x.GetTurbo("Pesho"))
                .Returns(parts);

            var result = pilots.Object.GetTurbo("Petyr");

            Assert.Equal(null, result);

        }


        //InMemoryTest
        [Fact]
        public void CorrectGetNewAerodynamics()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new ApplicationDbContext(options.Options);
            var cars = new CarServices(repository);

            var parts = new PartsCars
            {
                Id = 1,
                Name = "Aerodynamics",
                Price = 100,
                Speed = 40,
                Strength = 50,
                IsDeleted = false,
            };

            var car = new Cars
            {
                EngineId = 1,
                GearboxId = 1,
                TeamId = 2,
                AerodynamicsId = 1,
                BrakesId = 1,
                ModelCarId = 1,
                MountingId = 1,
                TurboId = null,
                Id = 1,
            };

            cars.GetNewAerodynamics(parts, car);

            var newCar = cars.GetCar(1);

            var resultSpeed = newCar.Aerodynamics.Speed;

            Assert.Equal(40, resultSpeed);
        }

    }
}

