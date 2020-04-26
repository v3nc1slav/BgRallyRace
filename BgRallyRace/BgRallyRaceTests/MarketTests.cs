namespace BgRallyRaceTests
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using BgRallyRace.Models.Enums;
    using BgRallyRace.Services;
    using BgRallyRace.Services.Market;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class MarketTests
    {
        //MockTests
        [Fact]
        public void CorrectGetPilotsForMarket()
        {
            var pilots = new Mock<IMarketServices>();
            pilots.Setup(x => x.GetPilotsForMarket(It.IsAny<int>()))
                .Returns(new List<RallyPilots>
                {
                    new RallyPilots{FirstName="Gosho", LastName="Ivanov" },
                    new RallyPilots{Age=18, Pounds=80, }
                }) ;

            var result = pilots.Object.GetPilotsForMarket();
            var resultFirstName = result[0].FirstName;
            var resultPounds = result[1].Pounds;

            Assert.Equal("Gosho", resultFirstName);
            Assert.Equal(80, resultPounds);

            pilots.Verify(x => x.GetPilotsForMarket(1), Times.Once);
        }

        [Fact]
        public void CorrectGetNavigatorsForMarket()
        {
            var pilots = new Mock<IMarketServices>();
            pilots.Setup(x => x.GetNavigatorsForMarket(It.IsAny<int>()))
                .Returns(new List<RallyNavigators>
                {
                    new RallyNavigators{FirstName="Gosho", LastName="Ivanov" },
                    new RallyNavigators{Age=18, Pounds=80, }
                });

            var result = pilots.Object.GetNavigatorsForMarket();
            var resultLastName = result[0].LastName;
            var resultAge = result[1].Age;

            Assert.Equal("Ivanov", resultLastName);
            Assert.Equal(18, resultAge);

            pilots.Verify(x => x.GetNavigatorsForMarket(1), Times.Once);
        }

        [Fact]
        public void CorrectGetPartsForMarket()
        {
            var pilots = new Mock<IMarketServices>();
            pilots.Setup(x => x.GetPartsForMarket(It.IsAny<int>()))
                .Returns(new List<PartsCars>
                {
                    new PartsCars{ CarId=1,  Name = "Engine", Price=100, Speed=45, Strength=85, Type=PartsCarsType.Engines},
                    new PartsCars{ CarId=2,  Name = "Brakes", Price=150, Speed=100, Strength=100, Type=PartsCarsType.Brakes},
                });

            var result = pilots.Object.GetPartsForMarket();
            var resultName = result[0].Name;
            var resultSpeed = result[0].Speed;
            var resultType = result[0].Type;
            var resultId = result[1].CarId;
            var resultPrice = result[1].Price;
            var resultStrength = result[1].Strength;

            Assert.Equal("Engine", resultName);
            Assert.Equal(45, resultSpeed);
            Assert.Equal(PartsCarsType.Engines, resultType);
            Assert.Equal(2, resultId);
            Assert.Equal(150, resultPrice);
            Assert.Equal(100, resultStrength);

            pilots.Verify(x => x.GetPartsForMarket(1), Times.Once);
        }

        [Fact]
        public void CorrectTotalPilots()
        {
            var pilots = new Mock<IMarketServices>();
            pilots.Setup(x => x.TotalPilots())
                .Returns(60);

            var result = pilots.Object.TotalPilots();

            Assert.Equal(60, result);

            pilots.Verify(x => x.TotalPilots(), Times.Once);
        }

        [Fact]
        public void CorrectTotalNavigators()
        {
            var pilots = new Mock<IMarketServices>();
            pilots.Setup(x => x.TotalNavigators())
                .Returns(40);

            var result = pilots.Object.TotalNavigators();

            Assert.Equal(40, result);

            pilots.Verify(x => x.TotalNavigators(), Times.Once);
        }

        [Fact]
        public void CorrectTotalParts()
        {
            var pilots = new Mock<IMarketServices>();
            pilots.Setup(x => x.TotalParts())
                .Returns(10);

            var result = pilots.Object.TotalParts();

            Assert.Equal(10, result);

            pilots.Verify(x => x.TotalParts(), Times.Once);
        }

        //InMemoryTest
        [Fact]
        public void CorrectRentalsPilot()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new ApplicationDbContext(options.Options);
            var marcet = new MarketServices(repository);
            var money = new MoneyAccountServices(repository);
            var pilot = new RallyPilotsServices(repository);
            var navigator = new RallyNavigatorsServices(repository);
            var car = new CarServices(repository, pilot, navigator, money);
            var team = new TeamServices(repository, car, money, pilot, navigator);

            money.CreateMoneyAccount("Pesho");
            team.CreateTeamAsync("Vidin", "Pesho");
            marcet.RentalsPilot(3,"Pesho",100m);

            var resultPilotId = team.FindUserAsync(1).RallyPilotId;

            Assert.Equal(3, resultPilotId);
        }

        [Fact]
        public void CorrectReplacedOldPartWhithNew()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new ApplicationDbContext(options.Options);
            var market = new MarketServices(repository);

            var part = new PartsCars { Name = "Engen", Type = PartsCarsType.Engines, 
                Speed = 100, Strength = 100, CarId = 1, Price= 100 , IsDeleted=false, Id=1,};
            var car = new Cars { EngineId = 1, GearboxId = 1, TeamId = 2, AerodynamicsId = 1, BrakesId =1,
             ModelCarId = 1, MountingId= 1, TurboId= null, Id=1};

            //for Copy
        }
    }
}
